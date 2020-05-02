/*!
 * jQuery Slider Evolution - for jQuery 1.3+
 * http://codecanyon.net/user/aeroalquimia/portfolio?ref=aeroalquimia
 *
 * Copyright 2011, Eduardo Daniel Sada
 * http://codecanyon.net/wiki/buying/howto-buying/licensing/
 *
 * Version: 1.1.5 (08 Jun 2011)
 *
 * Includes jQuery Easing v1.3
 * http://gsgd.co.uk/sandbox/jquery/easing/
 * Copyright (c) 2008 George McGinley Smith
 * jQuery Easing released under the BSD License.
 */

(function($) {
  
  var ie6 = (jQuery.browser.msie && parseInt(jQuery.browser.version, 10) < 7 && parseInt(jQuery.browser.version, 10) > 4);

  if (ie6)
  {
    try { document.execCommand("BackgroundImageCache", false, true); } catch(err) {}
  };

  if ($.proxy === undefined)
  {
    $.extend({
      proxy: function( fn, thisObject ) {
        if ( fn ) {
          proxy = function() { return fn.apply( thisObject || this, arguments ); };
        };
        return proxy;
      }
    });
  };
  
  $.extend( $.easing, {
    easeOutCubic: function (x, t, b, c, d) {
      return c*((t=t/d-1)*t*t + 1) + b;
    }
  });
  
  SliderObject = function(el, options) {
    this.create(el, options);
  };

  $.extend(SliderObject.prototype, {

      version   : "1.1.4",

      create: function(el, options) {
      
        this.defaults = {
          name            : 'jquery-slider',

          navigation      : true,
          selector        : true,
          timer           : true,
          control         : true,
          
          pauseOnClick    : true,
          pauseOnHover    : true,
          loop            : true,
          slideshow       : true,

          delay           : 4500, // in ms
          duration        : 400, // in ms
          bars            : 9,
          columns         : 7,
          rows            : 3,
          speed           : 80, // in ms
          padding         : 8,
          
          easing          : "easeOutCubic",

          //direction       : 'alternate', // left, right, alternate, random
          transition      : 'random',
          onComplete      : function() {},
          onSlideshowEnd  : function() {}
        };
        this.options      = {};
        this.transitions  = [
          'fade', 'square', 'bar', 'squarerandom', 'fountain', 'rain',
        ];
        this.dom          = {};
        this.img          = [];
        this.titles       = [];
        this.links        = [];
        this.imgInc       = 0;
        this.imgInterval  = 0;
        this.inc          = 0;
        this.order        = [];
        this.resto        = 0;
        this.selectores   = [];
        this.direction    = 0;
        this.degrees      = 0;
        this.timer        = 0;
        this.slides       = [];
        this.esqueleto    = {
          wrapper     : [],
          navigation  : [],
          timer       : [],
          selector    : [],
          control     : [],
          clock       : []
        };
        
        this.events = 
        {
          clicked : false,
          hovered : false,
          playing : false,
          paused  : false,
          stopped : false
        };

        this.element = $(el);

        var params = this.options;
        var self   = this;

        var slides = this.element.children("div");

        if (slides.length < 2) {
          return false;
        }

        if (!options['width']) {
          options['width']  = 0;
          options['height'] = 0;

          var temp = {};
          
          // Recorrer todos los slides y setear la clase con el mayor tamaño encontrado
          slides.children().each(function() {
            if ($(this).is("img")) {
              temp['width']  = $(this).outerWidth();
              temp['height'] = $(this).outerHeight();
              
              options['width']  = (temp['width'] >= options['width']) ? temp['width'] : 0;
              options['height'] = (temp['height'] >= options['height']) ? temp['height'] : 0;
            }
          });
          delete temp;

          if (options['width']==0 || options['height']==0) {
            delete options['width'];
            delete options['height'];
          }
        }

        this.options = $.extend(true, this.defaults, options);

        var optionClass = this.options.name + '-option';
        $.each(['navigation', 'selector', 'control', 'timer'], function(i, s) {
          if (self.options[s]) {
            optionClass += '-' + s ;
          }
        });
        
        // Crear la maqueta HTML
        this.esqueleto.wrapper = this.element.wrap('<div class="' + this.options.name + '-wrapper '+ optionClass +'" />').parent();
        this.esqueleto.wrapper.css({'width': this.options.width, 'height': this.options.height});
        
        this.element.css({'width': this.options.width, 'height': this.options.height, 'overflow': 'hidden', 'position': 'relative'});
        
        // Recorrer todos los slides y asignarles las clases correspondientes
        slides.each(function(i) {
          if (i == 0) {
            $(this).addClass(self.options.name + '-slide-current');
          }
          $(this).addClass(self.options.name + '-slide');
          $(this).addClass(self.options.name + '-slide-' + (i+1));
          
          // Crear la lista de selectores (1 2 3 4)
          self.selectores = $(self.selectores).add($('<a href="#" class="' + self.options.name + '-selector" rel="'+(i+1)+'"><span class="' + self.options.name + '-selector-span ' + self.options.name + '-selector-'+(i+1)+'"><span>'+(i+1)+'</span></span></a>'));
          if (i == 0) {
            $(self.selectores).addClass(self.options.name + '-selector-current');
          }
        });
        
        // Agregar la lista de selectores al wrapper
        this.esqueleto.selector = $('<div class="' + this.options.name + '-selectors" />').insertAfter(el);
        this.esqueleto.selector.append(this.selectores);
        
        if (!this.options.selector) {
          this.esqueleto.selector.hide();
        } else {
          if (this.rgbToHex(this.esqueleto.selector.css("color"))=="#FFFFFF") {
            var ouWidth = $('.' + this.options.name + '-selector').outerWidth(true);
            ouWidth = -((ouWidth * slides.length) / 2);
            this.esqueleto.selector.css({"margin-left": ouWidth});
          }
        }
        
        if (this.options.navigation) {
          this.esqueleto.navigation = $('<div class="' + this.options.name + '-navigation" />').insertAfter(el);
          var arrowPrev = $('<a href="#" class="' + this.options.name + '-navigation-prev" rel="-1"><span>Prev</span></a>');
          var arrowNext = $('<a href="#" class="' + this.options.name + '-navigation-next" rel="+1"><span>Next</span></a>');
          this.esqueleto.navigation.append(arrowPrev, arrowNext);
        }
        
        if (this.options.control) {
          this.esqueleto.control = $('<a href="#" class="' + this.options.name + '-control ' + this.options.name + '-control-pause"><span>Play/Pause</span></a>').insertAfter(el);
        }
        
        if (this.options.timer) {
          this.esqueleto.timer          = $('<div class="' + this.options.name + '-timer"></div>').insertAfter(el);
          this.esqueleto.clock.mask     = $('<div class="' + this.options.name + '-timer-mask"></div>');
          this.esqueleto.clock.rotator  = $('<div class="' + this.options.name + '-timer-rotator"></div>');
          this.esqueleto.clock.bar      = $('<div class="' + this.options.name + '-timer-bar"></div>');
          this.esqueleto.clock.command  = this.rgbToHex(this.esqueleto.timer.css("color"));
          this.esqueleto.timer.append(this.esqueleto.clock.mask.append(this.esqueleto.clock.rotator), this.esqueleto.clock.bar);
        }
        
        this.addEvents();
        
        if (this.options.slideshow) {
          this.startTimer();
        } else {
          this.stopTimer();
        }
        
      },
      
      addEvents: function() {
        var self    = this;
        var wrapper = this.esqueleto.wrapper;
        var options = this.options;

        wrapper.hover(function() {
          wrapper.addClass(options.name + '-hovered');
          if (options.pauseOnHover && !self.events.paused) {
            self.events.hovered = true;
            self.pauseTimer();
          }
        }, function() {
          wrapper.removeClass(options.name + '-hovered');
          if (options.pauseOnHover && self.events.hovered) {
            self.startTimer();
          }
          self.events.hovered = false;
        });
  
        this.esqueleto.selector.children("a").click(function(event) {
          // Solo iremos al slide seleccionado si no esta reproduciendose
          if (self.events.playing == false) {
            if ($(this).hasClass(options.name + '-selector-current') == false) {
              var stopped = self.events.stopped;
              self.stopTimer();
              self.callSlide($(this).attr('rel'));
              if (!options.pauseOnClick && !stopped) {
                self.startTimer();
              }
            }
          }
          event.preventDefault();
        });

        if (options.navigation) {
          this.esqueleto.navigation.children("a").click(function(event) {
            if (self.events.playing == false) {
              var stopped = self.events.stopped;
              self.stopTimer();
              
              self.callSlide($(this).attr("rel"));
              if (!options.pauseOnClick && !stopped) {
                self.startTimer();
              }
            }
            event.preventDefault();
          });
        };

        if (options.control) {
          this.esqueleto.control.click($.proxy(function(event) {
            if (this.events.stopped) {
              this.startTimer();
            } else {
              this.stopTimer();
            }
            this.events.hovered = false;
            event.preventDefault();
          }, this));
        };

      },

      startTimer: function() {
        if (this.options.timer) {
          // usar el timer
          
          if (this.esqueleto.clock.command == "#000000") {
            // timer barra
            this.esqueleto.clock.bar.animate({"width": "100%"}, (this.resto > 0 ? this.resto : this.options.delay), "linear", $.proxy(function() {
              this.callSlide("+1");
              this.resto = 0;
              this.esqueleto.clock.bar.css({"width": 0});
              this.startTimer();
            }, this));
            
          } else if (this.esqueleto.clock.command = "#FFFFFF") {
            // timer circular
            
            this.timer = setInterval($.proxy(function() {

                var degreeCSS = "rotate("+this.degrees+"deg)";
                this.degrees += 2;
                this.esqueleto.clock.rotator.css({
                  "-webkit-transform" : degreeCSS,
                  "-moz-transform"    : degreeCSS,
                  "-o-transform"      : degreeCSS,
                  "transform"         : degreeCSS
                });
                
                if (jQuery.browser.msie) {
                  this.esqueleto.clock.rotator.get(0).style['msTransform'] = degreeCSS;
                }

                if(this.degrees > 180) {
                  this.esqueleto.clock.rotator.addClass(this.options.name + '-timer-rotator-move');
                  this.esqueleto.clock.mask.addClass(this.options.name + '-timer-mask-move');
                }
                if(this.degrees > 360) {
                  this.esqueleto.clock.rotator.removeClass(this.options.name + '-timer-rotator-move');
                  this.esqueleto.clock.mask.removeClass(this.options.name + '-timer-mask-move');
                  this.degrees = 0;
                  this.callSlide("+1");
                  this.resto = 0;
                }

            }, this), this.options.delay/180);

          }
          
          if (this.options.control) {
            this.esqueleto.control.removeClass(this.options.name + '-control-play');
            this.esqueleto.control.addClass(this.options.name + '-control-pause');
          }

        } else {
          // usar el intervalo de js

          if (!this.timer) {
            this.timer = setInterval($.proxy(function() {
              this.callSlide("+1");
            }, this), this.options.delay);
          }
        
        }

        this.events.paused  = false;
        this.events.stopped = false;
        
        this.element.trigger("sliderPlay");
        
      },
      
      pauseTimer: function() {
        clearInterval(this.timer);
        this.timer = "";
        if (this.options.timer) {
          this.esqueleto.clock.bar.stop(true);
          var percent = 100 - (parseInt(this.esqueleto.clock.bar.css("width"), 10) * 100 / this.options.width);
          this.resto = this.options.delay * percent / 100;
        }
        
        this.events.paused = true;

        if (this.options.control && !this.events.hovered) {
          this.esqueleto.control.removeClass(this.options.name + '-control-pause');
          this.esqueleto.control.addClass(this.options.name + '-control-play');
        }

        this.element.trigger("sliderPause");
      },
      
      stopTimer: function() {
        clearInterval(this.timer);
        this.timer = "";
        if (this.options.timer) {
          this.esqueleto.clock.bar.stop();
          this.resto = 0;

          if (this.esqueleto.clock.command == "#000000") {
            this.esqueleto.clock.bar.css({"width": 0});
          } else if(this.esqueleto.clock.command == "#FFFFFF") {
            this.esqueleto.clock.rotator.removeClass(this.options.name + '-timer-rotator-move');
            this.esqueleto.clock.mask.removeClass(this.options.name + '-timer-mask-move');
            this.degrees = 0;
            var degreeCSS = "rotate("+this.degrees+"deg)";
            this.esqueleto.clock.rotator.css({
              "-webkit-transform" : degreeCSS,
              "-moz-transform"    : degreeCSS,
              "-o-transform"      : degreeCSS,
              "transform"         : degreeCSS
            });

            if (jQuery.browser.msie) {
              this.esqueleto.clock.rotator.get(0).style['msTransform'] = degreeCSS;
            }
          }
        }
        
        this.events.paused  = true;
        this.events.stopped = true;
        this.events.hovered = false;

        if (this.options.control) {
          this.esqueleto.control.removeClass(this.options.name + '-control-pause');
          this.esqueleto.control.addClass(this.options.name + '-control-play');
        }

        this.element.trigger("sliderStop");
      },

      shuffle: function(arr) {
        for(var j, x, i = arr.length; i; j = parseInt(Math.random() * i, 10), x = arr[--i], arr[i] = arr[j], arr[j] = x) {}
        return arr;
      },

      rgbToHex: function(rgb) {
        if (rgb.match(/^#[0-9A-Fa-f]{6}$/)) {
          return rgb.toUpperCase();
        }
        var rgbvals = /rgb\((.+),(.+),(.+)\)/i.exec(rgb);
        if (!rgbvals) {
          return rgb.toUpperCase();
        }
        var rval = parseInt(rgbvals[1]);
        var gval = parseInt(rgbvals[2]);
        var bval = parseInt(rgbvals[3]);
        var pad = function(value) {
          return ((value.length < 2 ? '0' : '') + value).toUpperCase();
        };
        return ('#' + pad(rval.toString(16)) + pad(gval.toString(16)) + pad(bval.toString(16))).toUpperCase();
      },
      
      callSlide: function(slide) {
        if (this.events.playing) {
          return false;
        }
      
        var curSlide = this.element.children("." + this.options.name + '-slide-current');
        var curSel   = this.esqueleto.selector.children("." + this.options.name + '-selector-current');

        if (slide == "+1") {
          var nxtSlide = curSlide.next("." + this.options.name + '-slide');
          var nxtSel   = curSel.next();
          
          if (nxtSlide.length <= 0) {
            if (this.options.loop) {
              nxtSlide = this.element.children("." + this.options.name + '-slide').first();
              nxtSel  = this.selectores.first("a");
            } else {
              this.stopTimer();
              return false;
            }
          }
        } else if (slide == "-1") {
          var nxtSlide = curSlide.prev("." + this.options.name + '-slide');
          var nxtSel   = curSel.prev("a");
          
          if (nxtSlide.length <= 0) {
            nxtSlide = this.element.children("." + this.options.name + '-slide').last();
            nxtSel  = this.selectores.last("a");
          }
        } else {
          var nxtSlide = this.element.children("." + this.options.name + '-slide-' + slide);
          var nxtSel   = this.esqueleto.selector.children("." + this.options.name + '-selector[rel=' + slide + ']');
        }
        
        this.transition(curSlide, curSel, nxtSlide, nxtSel);
        this.element.trigger("sliderChange", nxtSlide);
      },
      
      transition: function(curSlide, curSel, nxtSlide, nxtSel)
      {
        if ($.isArray(this.options.transition)) {
          this.transitions = this.options.transition;
          this.options.transition = "random";
        }
        
        var nxtTrans = nxtSlide.attr('class').split(" ")[0].split(this.options.name + "-trans-")[1];
        if (nxtTrans === undefined) {
          nxtTrans = this.options.transition;
        }
        
        if (nxtTrans == "random") {
          // No usar la misma transicion anterior, siempre elegir una nueva
          var tmpTrans = '';
          while (tmpTrans == this.lastTransition || tmpTrans == '') {
            tmpTrans = this.shuffle(this.transitions)[0].toLowerCase();
          }
          nxtTrans = tmpTrans;
        }
        
        nxtTrans = nxtTrans.toLowerCase();
        this.lastTransition = nxtTrans;
        
        this["trans" + nxtTrans](curSlide, curSel, nxtSlide, nxtSel);
      },
      
      transfade: function(curSlide, curSel, nxtSlide, nxtSel)
      {
        this.events.playing = true;
        
        nxtSlide.css({"opacity" : 1}).addClass(this.options.name + '-slide-next');
        curSel.removeClass(this.options.name + '-selector-current');
        nxtSel.addClass(this.options.name + '-selector-current');
          
        curSlide.stop().animate({"opacity" : 0}, this.options.duration, $.proxy(function() {
          curSlide.removeClass(this.options.name + '-slide-current');
          nxtSlide.addClass(this.options.name + '-slide-current');
          nxtSlide.removeClass(this.options.name + '-slide-next');
          
          this.element.trigger("sliderTransitionFinishes", nxtSlide);
          this.events.playing = false;
        }, this));
        
      },
      
      transbar: function(curSlide, curSel, nxtSlide, nxtSel, options)
      {
        options = $.extend(true, {'direction':'left'}, options);
        this.events.playing = true;
        
        var cssbar = {
          'width'   : Math.round(this.options.width / this.options.bars),
          'height'  : this.options.height
        };
        
        bar_array = new Array(this.options.bars);
        
        if (options['direction']=="right") {
          c = 0;
          for (i = this.options.bars; i > 0; i--) {
            bar_array[c] = i;
            c++;
          }
        } else if (options['direction']=="left") {
          for (i = 1; i <= this.options.bars; i++) {
            bar_array[i] = i;
          }
        } else if (options['direction']=="fountain" || options['direction']=="rain") {
          var odd    = 1;
          var middle = parseInt(this.options.bars/2);
          
          for (i = 1; i <= this.options.bars; i++) {
            bar_array[i-1] = (middle - (parseInt((i)/2)*odd))+1;
            odd *= -1;
          }
        }
        
        $.each(bar_array, $.proxy(function(i, el) {
          position = (el * cssbar.width) - cssbar.width;
          
          bar = $('<div class="' + this.options.name + '-bar ' + this.options.name + '-bar-' + el + '"/>');
          bar.css({
            'position'  : 'absolute',
            'overflow'  : 'hidden',
            'left'      : position,
            'z-index'   : 3,
            'opacity'   : 0,
            'background-position': '-' + position + 'px top'
          }).css(cssbar);
          
          if (options['direction']=="fountain") {
            bar.css({'top': this.options.height});
          } else if (options['direction']=="rain") {
            bar.css({'top': -this.options.height});
          }
          
          bar.append('<div style="position: absolute; left: -'+position+'px; width: '+this.options.width+'px; height: '+this.options.height+'px;">'+nxtSlide.html()+'</div>');
          this.element.append(bar);
          
          delay = this.options.speed * i;
          bar.animate({'opacity':0}, delay).animate({'opacity': 1, 'top': 0}, {duration: this.options.duration});
        }, this));
        
        curSel.removeClass(this.options.name + '-selector-current');
        nxtSel.addClass(this.options.name + '-selector-current');
        
        setTimeout($.proxy(function() {
          nxtSlide.css({"opacity" : 1}).addClass(this.options.name + '-slide-current');
          curSlide.css({"opacity" : 0}).removeClass(this.options.name + '-slide-current');
          
          this.element.children("." + this.options.name + '-bar').remove();

          this.events.playing = false;
          this.element.trigger("sliderTransitionFinishes", nxtSlide);
        }, this), delay + this.options.duration);
        
      },
      
      transbarleft: function(curSlide, curSel, nxtSlide, nxtSel)
      {
        return this.transbar(curSlide, curSel, nxtSlide, nxtSel);
      },

      transbarright: function(curSlide, curSel, nxtSlide, nxtSel)
      {
        return this.transbar(curSlide, curSel, nxtSlide, nxtSel, {"direction": "right"});
      },
      
      transsquare: function(curSlide, curSel, nxtSlide, nxtSel, options)
      {
        options = $.extend(true, {'mode':'acumulative', 'effect':'rain'}, options);
        this.events.playing = true;
        
        nxtSlide.css({"opacity" : 1});
        curSel.removeClass(this.options.name + '-selector-current');
        nxtSel.addClass(this.options.name + '-selector-current');
        
        //set vars
        var row_width  = Math.round(this.options.width  / this.options.columns);
        var row_height = Math.round(this.options.height / this.options.rows);
        
        var square_array = [];
        
        var nxtSlide_html = nxtSlide.html();
        
        for (iRow = 1; iRow <= this.options.rows; iRow++)
        {
          for (iCol = 1; iCol <= this.options.columns; iCol++)
          {
            square_array.push(iCol+''+iRow);
          
            var block_top = ((iRow * row_height) - row_height);
            var block_left = ((iCol * row_width) - row_width);
            
            var position_left = (row_width * iCol) - row_width;
            var position_top  = (row_height * iRow) - row_height;

            var block = $('<div class="' + this.options.name + '-block ' + this.options.name + '-block-'+iCol+iRow+'" />');
            
            block.css({
              'overflow': 'hidden',
              'position': 'absolute',
              'width'   : row_width,
              'height'  : row_height,
              'z-index' : 3,
              'top'     : block_top,
              'left'    : block_left,
              'opacity' : 0,
              'background-position': '-'+position_left+'px -'+position_top+'px'
            });
            
            block.append('<div style="position: absolute; left: -'+position_left+'px; top: -'+position_top+'px; width: '+this.options.width+'px; height: '+this.options.height+'px;">'+nxtSlide_html+'</div>');
            
            this.element.append(block);
          }
        }
        
        if (options['effect'] == 'random') {
          square_array = this.shuffle(square_array);
        } else if (options['effect'] == 'swirl') {
          square_array = this.arrayswirl(square_array);
        }
        
        if (options['mode'] == 'acumulative') {
        
          var iSquare = 0;
          for (iRow = 1; iRow <= this.options.rows; iRow++)
          {
            colRow = iRow;
            for (iCol = 1; iCol <= this.options.columns; iCol++)
            {
              delay = this.options.speed * colRow;
              this.element.children('.' + this.options.name + '-block-' + square_array[iSquare]).animate({'width': row_width}, delay).animate({'opacity': 1}, this.options.duration);
              iSquare++;
              colRow++;
            }
          }
          
        } else if (options['mode'] == 'dual') {
          
          $.each(square_array, $.proxy(function(i, el) {
            delay = this.options.speed * i;
            this.element.children('.' + this.options.name + '-block-' + el).animate({'width': row_width}, delay).animate({'opacity': 1}, this.options.duration);
          }, this));
        
        } else if (options['mode'] == 'lineal') {
          
          $.each(square_array, $.proxy(function(i, el) {
            delay = this.options.speed * i;
            this.element.children('.' + this.options.name + '-block-' + el).animate({'width': row_width}, delay).animate({'opacity': 1}, this.options.duration);
          }, this));
        
        }

        
        setTimeout($.proxy(function() {
          nxtSlide.css({"opacity" : 1}).addClass(this.options.name + '-slide-current');
          curSlide.css({"opacity" : 0}).removeClass(this.options.name + '-slide-current');
          
          this.element.children("." + this.options.name + '-block').remove();

          this.events.playing = false;
          this.element.trigger("sliderTransitionFinishes", nxtSlide);
        }, this), delay + this.options.duration);

      },

      transsquarerandom: function(curSlide, curSel, nxtSlide, nxtSel)
      {
        return this.transsquare(curSlide, curSel, nxtSlide, nxtSel, {'effect': 'random'});
      },
      
      transslide: function(curSlide, curSel, nxtSlide, nxtSel, options)
      {
        options = $.extend(true, {'direction':'left'}, options);
        this.events.playing = true;
        
        nxtSlide.css({"opacity" : 1});
        curSel.removeClass(this.options.name + '-selector-current');
        nxtSel.addClass(this.options.name + '-selector-current');

        curSlide.removeClass(this.options.name + '-slide-current');
        curSlide.addClass(this.options.name + '-slide-next');
        nxtSlide.addClass(this.options.name + '-slide-current');
        
        if (options.direction == "left") {
          nxtSlide.css({"left":this.options.width});
        } else if (options.direction == "right") {
          nxtSlide.css({"left":-this.options.width});
        } else if (options.direction == "top") {
          nxtSlide.css({"top":-this.options.height});
        } else if (options.direction == "bottom") {
          nxtSlide.css({"top":this.options.height});
        }
        
        nxtSlide.stop().animate({"left": 0, "top": 0}, this.options.duration, this.options.easing, $.proxy(function() {
          curSlide.removeClass(this.options.name + '-slide-next');
          curSlide.css({"opacity" : 0});
          this.events.playing = false;
          this.element.trigger("sliderTransitionFinishes", nxtSlide);
        }, this));
      },
      
      transslideleft: function(curSlide, curSel, nxtSlide, nxtSel)
      {
        return this.transslide(curSlide, curSel, nxtSlide, nxtSel, {'direction': 'left'});
      },

      transslideright: function(curSlide, curSel, nxtSlide, nxtSel)
      {
        return this.transslide(curSlide, curSel, nxtSlide, nxtSel, {'direction': 'right'});
      },

      transslidetop: function(curSlide, curSel, nxtSlide, nxtSel)
      {
        return this.transslide(curSlide, curSel, nxtSlide, nxtSel, {'direction': 'top'});
      },

      transslidebottom: function(curSlide, curSel, nxtSlide, nxtSel)
      {
        return this.transslide(curSlide, curSel, nxtSlide, nxtSel, {'direction': 'bottom'});
      },
      
      transfountain: function(curSlide, curSel, nxtSlide, nxtSel)
      {
        return this.transbar(curSlide, curSel, nxtSlide, nxtSel, {'direction': 'fountain'});
      },
      
      transrain: function(curSlide, curSel, nxtSlide, nxtSel)
      {
        return this.transbar(curSlide, curSel, nxtSlide, nxtSel, {'direction': 'rain'});
      },
      
      transexplode: function(curSlide, curSel, nxtSlide, nxtSel, options)
      {
        options = $.extend(true, {'mode':'acumulative', 'effect':'rain'}, options);
        this.events.playing = true;
        
        nxtSlide.css({"opacity" : 0});
        curSel.removeClass(this.options.name + '-selector-current');
        nxtSel.addClass(this.options.name + '-selector-current');
        
        //set vars
        var row_width  = Math.round(this.options.width  / this.options.columns);
        var row_height = Math.round(this.options.height / this.options.rows);
        
        var square_array = [];
        
        var nxtSlide_html = nxtSlide.html();
        
        for (iRow = 1; iRow <= this.options.rows; iRow++)
        {
          for (iCol = 1; iCol <= this.options.columns; iCol++)
          {
            square_array.push(iCol+''+iRow);
            
            var block_top   = ((iRow * row_height) - row_height);
            var block_left  = ((iCol * row_width) - row_width);
            
            var position_left = (row_width * iCol) - row_width;
            var position_top  = (row_height * iRow) - row_height;

            var pad_left = (iCol- parseInt((this.options.columns+1)/2)) * this.options.padding;
            var pad_top  = (iRow- parseInt((this.options.rows+1)/2)) * this.options.padding;

            var block = $('<div class="' + this.options.name + '-block-clon ' + this.options.name + '-block-clon-'+iCol+iRow+'" />');
            
            block.css({
              'overflow': 'hidden',
              'position': 'absolute',
              'width'   : row_width,
              'height'  : row_height,
              'z-index' : 2,
              'top'     : block_top + pad_top,
              'left'    : block_left + pad_left,
              'opacity' : 0,
              'background-position': '-'+position_left+'px -'+position_top+'px'
            });
            
            block.append('<div style="position: absolute; left: -'+position_left+'px; top: -'+position_top+'px; width: '+this.options.width+'px; height: '+this.options.height+'px;">'+nxtSlide_html+'</div>');
            
            this.element.append(block);

            var block = $('<div class="' + this.options.name + '-block ' + this.options.name + '-block-'+iCol+iRow+'" />');
            
            block.css({
              'overflow': 'hidden',
              'position': 'absolute',
              'width'   : row_width,
              'height'  : row_height,
              'z-index' : 3,
              'top'     : block_top,
              'left'    : block_left,
              'opacity' : 1,
              'background-position': '-'+position_left+'px -'+position_top+'px'
            });
            
            block.append('<div style="position: absolute; left: -'+position_left+'px; top: -'+position_top+'px; width: '+this.options.width+'px; height: '+this.options.height+'px;">'+curSlide.html()+'</div>');
            
            this.element.append(block);

          }
        }

        curSlide.css({"opacity" : 0});
        
        if (options['effect'] == 'random') {
          square_array = this.shuffle(square_array);
        } else if (options['effect'] == 'swirl') {
          square_array = this.arrayswirl(square_array);
        }
        
        for (iRow = 1; iRow <= this.options.rows; iRow++)
        {
          colRow = iRow;
          for (iCol = 1; iCol <= this.options.columns; iCol++)
          {
            delay = this.options.speed * colRow;

            var pad_left = (iCol- parseInt((this.options.columns+1)/2)) * this.options.padding;
            var pad_top  = (iRow- parseInt((this.options.rows+1)/2)) * this.options.padding;

            this.element.children('.' + this.options.name + '-block-' + iCol+''+iRow).animate({'left': '+='+pad_left, 'top': '+='+pad_top}, this.options.duration);
            colRow++;
          }
        }

        var max_delay = delay;
        var iSquare = 0;
        for (iRow = 1; iRow <= this.options.rows; iRow++)
        {
          colRow = iRow;
          for (iCol = 1; iCol <= this.options.columns; iCol++)
          {
            delay = this.options.speed * colRow;

            this.element.children('.' + this.options.name + '-block-' + square_array[iSquare]).animate({'opacity': 0}, delay);
            this.element.children('.' + this.options.name + '-block-clon-' + square_array[iSquare]).animate({'width': row_width}, this.options.duration).animate({'opacity': 1}, delay).animate({'width': row_width}, max_delay - delay);

            iSquare++;
            colRow++;
          }
        }

        for (iRow = 1; iRow <= this.options.rows; iRow++)
        {
          colRow = iRow;
          for (iCol = 1; iCol <= this.options.columns; iCol++)
          {
            delay = this.options.speed * colRow;

            var pad_left = (iCol- parseInt((this.options.columns+1)/2)) * this.options.padding;
            var pad_top  = (iRow- parseInt((this.options.rows+1)/2)) * this.options.padding;

            this.element.children('.' + this.options.name + '-block-clon-' + iCol+''+iRow).animate({'left': '-='+pad_left, 'top': '-='+pad_top}, this.options.duration);
            colRow++;
          }
        }

        setTimeout($.proxy(function() {
          nxtSlide.css({"opacity" : 1}).addClass(this.options.name + '-slide-current');
          curSlide.css({"opacity" : 0}).removeClass(this.options.name + '-slide-current');
          
          this.element.children("." + this.options.name + '-block').remove();
          this.element.children("." + this.options.name + '-block-clon').remove();

          this.events.playing = false;
          this.element.trigger("sliderTransitionFinishes", nxtSlide);
        }, this), (max_delay + (this.options.duration * 2)));
      
      },

      transexploderandom: function(curSlide, curSel, nxtSlide, nxtSel)
      {
        return this.transexplode(curSlide, curSel, nxtSlide, nxtSel, {'effect': 'random'});
      }
      
  });

	$.fn.slideshow = function(options, callback) {
    if (parseFloat($.fn.jquery) > 1.2) {
      var d = {};
      this.each(function() {
        var s = $(this);
        d = s.data("slider");
        if (!d) {
          d = new SliderObject(this, options, callback);
          s.data("slider", d);
        }
      });
      return d;
    } else {
      throw "The jQuery version that was loaded is too old. Slider Evolution requires jQuery 1.3+";
    }
  };

})(jQuery);