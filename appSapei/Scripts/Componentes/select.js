jQuery.fn.ComboFiltraComboValor = function (comboPadre) {
     return this.each(function () {
          var select = this;
          var options = [];
          $(select).find('option').each(function () {
               options.push({
                    value: $(this).val(),
                    text: $(this).text()
               });
          });
          $(select).data('options', options);

          $(comboPadre).change(function () {
               var options = $(select).empty().data('options');
               var valor = $(this).val().split("-")[0];
               var search = "-" + $.trim(valor);
               var regex = new RegExp(search, "gi");

               $.each(options, function (i) {
                    var option = options[i];
                    if (option.value.match(regex) !== null) {
                         $(select).append(
                           $('<option>').text(option.text).val(option.value)
                         );
                    }
               });
          });
     });
};

jQuery.fn.ComboFiltraCombo = function (comboPadre) {
     return this.each(function () {
          var select = this;
          var options = [];
          $(select).find('option').each(function () {
               options.push({
                    value: $(this).val(),
                    text: $(this).text()
               });
          });
          $(select).data('options', options);

          $(comboPadre).change(function () {
               var options = $(select).empty().data('options');
               var valor = $(this).val();
               var search = $.trim(valor);
               var regex = new RegExp(search, "gi");

               $.each(options, function (i) {
                    var option = options[i];
                    if (option.value.match(regex) !== null) {
                         $(select).append(
                           $('<option>').text(option.text).val(option.value)
                         );
                    }
               });
          });
     });
};