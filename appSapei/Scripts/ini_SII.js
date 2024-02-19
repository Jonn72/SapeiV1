/**
 * Resize function without multiple trigger
 * 
 * Usage:
 * $(window).smartresize(function(){  
 *     // code here
 * });
 */
(function ($, sr) {
     // debouncing function from John Hann
     // http://unscriptable.com/index.php/2009/03/20/debouncing-javascript-methods/
     var debounce = function (func, threshold, execAsap) {
          var timeout;

          return function debounced() {
               var obj = this, args = arguments;
               function delayed() {
                    if (!execAsap)
                         func.apply(obj, args);
                    timeout = null;
               }

               if (timeout)
                    clearTimeout(timeout);
               else if (execAsap)
                    func.apply(obj, args);

               timeout = setTimeout(delayed, threshold || 100);
          };
     };

     // smartresize 
     jQuery.fn[sr] = function (fn) { return fn ? this.bind('resize', debounce(fn)) : this.trigger(sr); };

})(jQuery, 'smartresize');
/**
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

var CURRENT_URL = window.location.href.split('#')[0].split('?')[0],
    $BODY = $('body'),
    $MENU_TOGGLE = $('#menu_toggle'),
    $SIDEBAR_MENU = $('#sidebar-menu'),
    $SIDEBAR_FOOTER = $('.sidebar-footer'),
    $LEFT_COL = $('.left_col'),
    $RIGHT_COL = $('.right_col'),
    $NAV_MENU = $('.nav_menu'),
    $FOOTER = $('footer');



// Sidebar
function init_sidebar() {
     // TODO: This is some kind of easy fix, maybe we can improve this
     var setContentHeight = function () {
          // reset height
          $RIGHT_COL.css('min-height', $(window).height());

          var bodyHeight = $BODY.outerHeight(),
               footerHeight = $BODY.hasClass('footer_fixed') ? -10 : $FOOTER.height(),
               leftColHeight = $LEFT_COL.eq(1).height() + $SIDEBAR_FOOTER.height(),
               contentHeight = bodyHeight < leftColHeight ? leftColHeight : bodyHeight;

          // normalize content
          contentHeight -= $NAV_MENU.height() + footerHeight;

          $RIGHT_COL.css('min-height', contentHeight);
     };

     $SIDEBAR_MENU.find('a').on('click', function (ev) {
          console.log('clicked - sidebar_menu');
          var $li = $(this).parent();

          if ($li.is('.active')) {
               $li.removeClass('active active-sm');
               $('ul:first', $li).slideUp(function () {
                    setContentHeight();
               });
          } else {
               // prevent closing menu if we are on child menu
               if (!$li.parent().is('.child_menu')) {
                    $SIDEBAR_MENU.find('li').removeClass('active active-sm');
                    $SIDEBAR_MENU.find('li ul').slideUp();
               } else {
                   if ($BODY.is(".nav-sm")) {
                       var temp = $SIDEBAR_MENU.find("li");
                         temp.find("li").removeClass("active active-sm");
                         temp.find("li ul").slideUp();
                    }
               }
               $li.addClass('active');

               $('ul:first', $li).slideDown(function () {
                    setContentHeight();
               });
          }
     });

     // toggle small or large menu 
     $MENU_TOGGLE.on('click', function () {
          console.log('clicked - menu toggle');

          if ($BODY.hasClass('nav-md')) {
               $SIDEBAR_MENU.find('li.active ul').hide();
               $SIDEBAR_MENU.find('li.active').addClass('active-sm').removeClass('active');
          } else {
               $SIDEBAR_MENU.find('li.active-sm ul').show();
               $SIDEBAR_MENU.find('li.active-sm').addClass('active').removeClass('active-sm');
          }

          $BODY.toggleClass('nav-md nav-sm');

          setContentHeight();

          $('.dataTable').each(function () { $(this).dataTable().fnDraw(); });
     });

     // check active menu
     $SIDEBAR_MENU.find('a[href="' + CURRENT_URL + '"]').parent('li').addClass('current-page');

     $SIDEBAR_MENU.find('a').filter(function () {
          return this.href == CURRENT_URL;
     }).parent('li').addClass('current-page').parents('ul').slideDown(function () {
          setContentHeight();
     }).parent().addClass('active');

     // recompute content when resizing
     $(window).smartresize(function () {
          setContentHeight();
     });

     setContentHeight();

     // fixed sidebar
     if ($.fn.mCustomScrollbar) {
          $('.menu_fixed').mCustomScrollbar({
               autoHideScrollbar: true,
               theme: 'minimal',
               mouseWheel: { preventDefault: true }
          });
     }
};
// /Sidebar

var randNum = function () {
     return (Math.floor(Math.random() * (1 + 40 - 20))) + 20;
};



// /Tooltip



// Switchery
$(document).ready(function () {
     if ($(".js-switch")[0]) {
          var elems = Array.prototype.slice.call(document.querySelectorAll('.js-switch'));
          elems.forEach(function (html) {
               var switchery = new Switchery(html, {
                    color: '#26B99A'
               });
          });
     }
});
// /Switchery


// Accordion
$(document).ready(function () {
     $(".expand").on("click", function () {
          $(this).next().slideToggle(200);
          $expand = $(this).find(">:first-child");

          if ($expand.text() == "+") {
               $expand.text("-");
          } else {
               $expand.text("+");
          }
     });
});



//hover and retain popover when on popover content
var originalLeave = $.fn.popover.Constructor.prototype.leave;
$.fn.popover.Constructor.prototype.leave = function (obj) {
     var self = obj instanceof this.constructor ?
          obj : $(obj.currentTarget)[this.type](this.getDelegateOptions()).data('bs.' + this.type);
     var container, timeout;

     originalLeave.call(this, obj);

     if (obj.currentTarget) {
          container = $(obj.currentTarget).siblings('.popover');
          timeout = self.timeout;
          container.one('mouseenter', function () {
               //We entered the actual popover – call off the dogs
               clearTimeout(timeout);
               //Let's monitor popover content instead
               container.one('mouseleave', function () {
                    $.fn.popover.Constructor.prototype.leave.call(self, self);
               });
          });
     }
};

$('body').popover({
     selector: '[data-popover]',
     trigger: 'click hover',
     delay: {
          show: 50,
          hide: 400
     }
});


function gd(year, month, day) {
     return new Date(year, month - 1, day).getTime();
}


/* AUTOCOMPLETE */

function init_autocomplete() {

     if (typeof (autocomplete) === 'undefined') { return; }
     console.log('init_autocomplete');

     var countries = { AD: "Andorra", A2: "Andorra Test", AE: "United Arab Emirates", AF: "Afghanistan", AG: "Antigua and Barbuda", AI: "Anguilla", AL: "Albania", AM: "Armenia", AN: "Netherlands Antilles", AO: "Angola", AQ: "Antarctica", AR: "Argentina", AS: "American Samoa", AT: "Austria", AU: "Australia", AW: "Aruba", AX: "Åland Islands", AZ: "Azerbaijan", BA: "Bosnia and Herzegovina", BB: "Barbados", BD: "Bangladesh", BE: "Belgium", BF: "Burkina Faso", BG: "Bulgaria", BH: "Bahrain", BI: "Burundi", BJ: "Benin", BL: "Saint Barthélemy", BM: "Bermuda", BN: "Brunei", BO: "Bolivia", BQ: "British Antarctic Territory", BR: "Brazil", BS: "Bahamas", BT: "Bhutan", BV: "Bouvet Island", BW: "Botswana", BY: "Belarus", BZ: "Belize", CA: "Canada", CC: "Cocos [Keeling] Islands", CD: "Congo - Kinshasa", CF: "Central African Republic", CG: "Congo - Brazzaville", CH: "Switzerland", CI: "Côte d’Ivoire", CK: "Cook Islands", CL: "Chile", CM: "Cameroon", CN: "China", CO: "Colombia", CR: "Costa Rica", CS: "Serbia and Montenegro", CT: "Canton and Enderbury Islands", CU: "Cuba", CV: "Cape Verde", CX: "Christmas Island", CY: "Cyprus", CZ: "Czech Republic", DD: "East Germany", DE: "Germany", DJ: "Djibouti", DK: "Denmark", DM: "Dominica", DO: "Dominican Republic", DZ: "Algeria", EC: "Ecuador", EE: "Estonia", EG: "Egypt", EH: "Western Sahara", ER: "Eritrea", ES: "Spain", ET: "Ethiopia", FI: "Finland", FJ: "Fiji", FK: "Falkland Islands", FM: "Micronesia", FO: "Faroe Islands", FQ: "French Southern and Antarctic Territories", FR: "France", FX: "Metropolitan France", GA: "Gabon", GB: "United Kingdom", GD: "Grenada", GE: "Georgia", GF: "French Guiana", GG: "Guernsey", GH: "Ghana", GI: "Gibraltar", GL: "Greenland", GM: "Gambia", GN: "Guinea", GP: "Guadeloupe", GQ: "Equatorial Guinea", GR: "Greece", GS: "South Georgia and the South Sandwich Islands", GT: "Guatemala", GU: "Guam", GW: "Guinea-Bissau", GY: "Guyana", HK: "Hong Kong SAR China", HM: "Heard Island and McDonald Islands", HN: "Honduras", HR: "Croatia", HT: "Haiti", HU: "Hungary", ID: "Indonesia", IE: "Ireland", IL: "Israel", IM: "Isle of Man", IN: "India", IO: "British Indian Ocean Territory", IQ: "Iraq", IR: "Iran", IS: "Iceland", IT: "Italy", JE: "Jersey", JM: "Jamaica", JO: "Jordan", JP: "Japan", JT: "Johnston Island", KE: "Kenya", KG: "Kyrgyzstan", KH: "Cambodia", KI: "Kiribati", KM: "Comoros", KN: "Saint Kitts and Nevis", KP: "North Korea", KR: "South Korea", KW: "Kuwait", KY: "Cayman Islands", KZ: "Kazakhstan", LA: "Laos", LB: "Lebanon", LC: "Saint Lucia", LI: "Liechtenstein", LK: "Sri Lanka", LR: "Liberia", LS: "Lesotho", LT: "Lithuania", LU: "Luxembourg", LV: "Latvia", LY: "Libya", MA: "Morocco", MC: "Monaco", MD: "Moldova", ME: "Montenegro", MF: "Saint Martin", MG: "Madagascar", MH: "Marshall Islands", MI: "Midway Islands", MK: "Macedonia", ML: "Mali", MM: "Myanmar [Burma]", MN: "Mongolia", MO: "Macau SAR China", MP: "Northern Mariana Islands", MQ: "Martinique", MR: "Mauritania", MS: "Montserrat", MT: "Malta", MU: "Mauritius", MV: "Maldives", MW: "Malawi", MX: "Mexico", MY: "Malaysia", MZ: "Mozambique", NA: "Namibia", NC: "New Caledonia", NE: "Niger", NF: "Norfolk Island", NG: "Nigeria", NI: "Nicaragua", NL: "Netherlands", NO: "Norway", NP: "Nepal", NQ: "Dronning Maud Land", NR: "Nauru", NT: "Neutral Zone", NU: "Niue", NZ: "New Zealand", OM: "Oman", PA: "Panama", PC: "Pacific Islands Trust Territory", PE: "Peru", PF: "French Polynesia", PG: "Papua New Guinea", PH: "Philippines", PK: "Pakistan", PL: "Poland", PM: "Saint Pierre and Miquelon", PN: "Pitcairn Islands", PR: "Puerto Rico", PS: "Palestinian Territories", PT: "Portugal", PU: "U.S. Miscellaneous Pacific Islands", PW: "Palau", PY: "Paraguay", PZ: "Panama Canal Zone", QA: "Qatar", RE: "Réunion", RO: "Romania", RS: "Serbia", RU: "Russia", RW: "Rwanda", SA: "Saudi Arabia", SB: "Solomon Islands", SC: "Seychelles", SD: "Sudan", SE: "Sweden", SG: "Singapore", SH: "Saint Helena", SI: "Slovenia", SJ: "Svalbard and Jan Mayen", SK: "Slovakia", SL: "Sierra Leone", SM: "San Marino", SN: "Senegal", SO: "Somalia", SR: "Suriname", ST: "São Tomé and Príncipe", SU: "Union of Soviet Socialist Republics", SV: "El Salvador", SY: "Syria", SZ: "Swaziland", TC: "Turks and Caicos Islands", TD: "Chad", TF: "French Southern Territories", TG: "Togo", TH: "Thailand", TJ: "Tajikistan", TK: "Tokelau", TL: "Timor-Leste", TM: "Turkmenistan", TN: "Tunisia", TO: "Tonga", TR: "Turkey", TT: "Trinidad and Tobago", TV: "Tuvalu", TW: "Taiwan", TZ: "Tanzania", UA: "Ukraine", UG: "Uganda", UM: "U.S. Minor Outlying Islands", US: "United States", UY: "Uruguay", UZ: "Uzbekistan", VA: "Vatican City", VC: "Saint Vincent and the Grenadines", VD: "North Vietnam", VE: "Venezuela", VG: "British Virgin Islands", VI: "U.S. Virgin Islands", VN: "Vietnam", VU: "Vanuatu", WF: "Wallis and Futuna", WK: "Wake Island", WS: "Samoa", YD: "People's Democratic Republic of Yemen", YE: "Yemen", YT: "Mayotte", ZA: "South Africa", ZM: "Zambia", ZW: "Zimbabwe", ZZ: "Unknown or Invalid Region" };

     var countriesArray = $.map(countries, function (value, key) {
          return {
               value: value,
               data: key
          };
     });

     // initialize autocomplete with custom appendTo
     $('#autocomplete-custom-append').autocomplete({
          lookup: countriesArray
     });

};

/* AUTOSIZE */

function init_autosize() {

     if (typeof $.fn.autosize !== 'undefined') {

          autosize($('.resizable_textarea'));

     }

};

/* PARSLEY */

function init_parsley() {

     if (typeof (parsley) === 'undefined') { return; }
     console.log('init_parsley');

     $/*.listen*/('parsley:field:validate', function () {
          validateFront();
     });
     $('#demo-form .btn').on('click', function () {
          $('#demo-form').parsley().validate();
          validateFront();
     });
     var validateFront = function () {
          if (true === $('#demo-form').parsley().isValid()) {
               $('.bs-callout-info').removeClass('hidden');
               $('.bs-callout-warning').addClass('hidden');
          } else {
               $('.bs-callout-info').addClass('hidden');
               $('.bs-callout-warning').removeClass('hidden');
          }
     };

     $/*.listen*/('parsley:field:validate', function () {
          validateFront();
     });
     $('#demo-form2 .btn').on('click', function () {
          $('#demo-form2').parsley().validate();
          validateFront();
     });
     var validateFront = function () {
          if (true === $('#demo-form2').parsley().isValid()) {
               $('.bs-callout-info').removeClass('hidden');
               $('.bs-callout-warning').addClass('hidden');
          } else {
               $('.bs-callout-info').addClass('hidden');
               $('.bs-callout-warning').removeClass('hidden');
          }
     };

     try {
          hljs.initHighlightingOnLoad();
     } catch (err) { }

};


/* INPUTS */

function onAddTag(tag) {
     alert("Added a tag: " + tag);
}

function onRemoveTag(tag) {
     alert("Removed a tag: " + tag);
}

function onChangeTag(input, tag) {
     alert("Changed a tag: " + tag);
}

/* VALIDATOR */

function init_validator() {

     if (typeof (validator) === 'undefined') { return; }
     console.log('init_validator');

     // initialize the validator function
     validator.message.date = 'not a real date';

     // validate a field on "blur" event, a 'select' on 'change' event & a '.reuired' classed multifield on 'keyup':
     $('form')
       .on('blur', 'input[required], input.optional, select.required', validator.checkField)
       .on('change', 'select.required', validator.checkField)
       .on('keypress', 'input[required][pattern]', validator.keypress);

     $('.multi.required').on('keyup blur', 'input', function () {
          validator.checkField.apply($(this).siblings().last()[0]);
     });

     $('form').submit(function (e) {
          e.preventDefault();
          var submit = true;

          // evaluate the form using generic validaing
          if (!validator.checkAll($(this))) {
               submit = false;
          }

          if (submit)
               this.submit();

          return false;
     });

};

/* PNotify */

function init_PNotify() {

     if (typeof (PNotify) === 'undefined') { return; }
     console.log('init_PNotify');

     new PNotify({
          title: "PNotify",
          type: "info",
          text: "Welcome. Try hovering over me. You can click things behind me, because I'm non-blocking.",
          nonblock: {
               nonblock: true
          },
          addclass: 'dark',
          styling: 'bootstrap3',
          hide: false,
          before_close: function (PNotify) {
               PNotify.update({
                    title: PNotify.options.title + " - Enjoy your Stay",
                    before_close: null
               });

               PNotify.queueRemove();

               return false;
          }
     });

};


/* CUSTOM NOTIFICATION */

function init_CustomNotification() {

     console.log('run_customtabs');

     if (typeof (CustomTabs) === 'undefined') { return; }
     console.log('init_CustomTabs');

     var cnt = 10;

     TabbedNotification = function (options) {
          var message = "<div id='ntf" + cnt + "' class='text alert-" + options.type + "' style='display:none'><h2><i class='fa fa-bell'></i> " + options.title +
             "</h2><div class='close'><a href='javascript:;' class='notification_close'><i class='fa fa-close'></i></a></div><p>" + options.text + "</p></div>";

          if (!document.getElementById('custom_notifications')) {
               alert('doesnt exists');
          } else {
               $('#custom_notifications ul.notifications').append("<li><a id='ntlink" + cnt + "' class='alert-" + options.type + "' href='#ntf" + cnt + "'><i class='fa fa-bell animated shake'></i></a></li>");
               $('#custom_notifications #notif-group').append(message);
               cnt++;
               CustomTabs(options);
          }
     };

     CustomTabs = function (options) {
          $('.tabbed_notifications > div').hide();
          $('.tabbed_notifications > div:first-of-type').show();
          $('#custom_notifications').removeClass('dsp_none');
          $('.notifications a').click(function (e) {
               e.preventDefault();
               var $this = $(this),
                 tabbed_notifications = '#' + $this.parents('.notifications').data('tabbed_notifications'),
                 others = $this.closest('li').siblings().children('a'),
                 target = $this.attr('href');
               others.removeClass('active');
               $this.addClass('active');
               $(tabbed_notifications).children('div').hide();
               $(target).show();
          });
     };

     CustomTabs();

     var tabid = idname = '';

     $(document).on('click', '.notification_close', function (e) {
          idname = $(this).parent().parent().attr("id");
          tabid = idname.substr(-2);
          $('#ntf' + tabid).remove();
          $('#ntlink' + tabid).parent().remove();
          $('.notifications a').first().addClass('active');
          $('#notif-group div').first().css('display', 'block');
     });

};

$(document).ready(function () {
    $(window).bind('scroll', function () {
        if ($(window).scrollTop() > 0) {
            $('.body .col-md-3.left_col').css('height', $('.body').height());
        }
    });

     init_sidebar();
     init_parsley();
     init_validator();
     init_PNotify();
     init_CustomNotification();
     init_autosize();
     init_autocomplete();

});

