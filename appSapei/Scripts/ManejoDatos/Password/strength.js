            var characters = 0;
            var capitalletters = 0;
            var loweletters = 0;
            var number = 0;
            var special = 0;
            var upperCase= new RegExp('[A-Z]');
            var lowerCase= new RegExp('[a-z]');
            var numbers = new RegExp('[0-9]');
            var specialchars = new RegExp('([!,%,&,@,#,$,^,*,?,_,~])');
            var total = 0;


                function check_strength(thisval){
                    if (thisval.length > 8) { characters = 1; } else { characters = 0; };
                    if (thisval.match(upperCase)) { capitalletters = 1; } else { capitalletters = 0; };
                    if (thisval.match(lowerCase)) { loweletters = 1; }  else { loweletters = 0; };
                    if (thisval.match(numbers)) { number = 1; } else { number = 0; };
                    if (thisval.match(specialchars)) { special = 1; } else { special = 0; };

                    if (capitalletters >= 1) {
                        $("#mayus").removeClass("label-danger"); $("#mayus").addClass("label-success");
                    }
                    else {
                        $("#mayus").removeClass("label-success"); $("#mayus").addClass("label-danger");
                    }

                    if (loweletters >= 1) {
                        $("#minus").removeClass("label-danger"); $("#minus").addClass("label-success");
                    }
                    else {
                        $("#minus").removeClass("label-success"); $("#minus").addClass("label-danger");
                    }

                    if (number >= 1) {
                        $("#num").removeClass("label-danger"); $("#num").addClass("label-success");
                    }
                    else {
                        $("#num").removeClass("label-success"); $("#num").addClass("label-danger");
                    }

                    if (special >= 1) {
                        $("#special").removeClass("label-danger"); $("#special").addClass("label-success");
                    }
                    else {
                        $("#special").removeClass("label-success"); $("#special").addClass("label-danger");
                    }

                    if (characters >= 1) {
                        $("#min").removeClass("label-danger"); $("#min").addClass("label-success");
                    }
                    else {
                        $("#min").removeClass("label-success"); $("#min").addClass("label-danger");
                    }

                    total = characters + capitalletters + loweletters + number + special;

                    console.log(total);

                }

            $('input[type="password"]').on('keyup keydown', function () {            
                var atrr = $(this).attr('data-password'); 
                var thisval = $('input[type="password"][data-password="' + atrr + '"]').val();
                $('input[type="text"][data-password="' + atrr + '"]').val(thisval);
                if (atrr === 'nuevo_password') { check_strength(thisval); }
                if ($('input[type="text"][data-password="nuevo_password"]').val() === $('input[type="text"][data-password="confirma_password"]').val() && $('input[type="text"][data-password="nuevo_password"]').val() != '') {
                    $("#coincidencia").removeClass("label-danger"); $("#coincidencia").addClass("label-success");
                }
                else {
                    $("#coincidencia").removeClass("label-success"); $("#coincidencia").addClass("label-danger");
                }

                console.log($('#coincidencia').attr('class'));

                if (total === 5 && $('#coincidencia').attr('class') === "label label-success") {
                    
                    $('#btnCambiar').removeAttr('disabled');
                }
                else {
                    $('#btnCambiar').attr('disabled', 'disabled');
                }
                       
            });

            $('input[type="text"]').on('keyup keydown', function() {
                var atrr = $(this).attr('data-password');
                var thisval = $('input[type="text"][data-password="' + atrr + '"]').val();
                console.log(thisval);
                $('input[type="password"][data-password="' + atrr+'"]').val(thisval);
                if (atrr === 'nuevo_password') { check_strength(thisval); } 
            });

                $("a").on('click', function(e) {

                    var atr = $(this).attr('data-password-button');
                    //console.log(atr);
                    //console.log($('input[type="text"][data-password="' + atr + '"]').attr('style'));
                    if ($('input[type="text"][data-password="' + atr + '"]').attr('style') === 'display: none;') {
                        $('input[type="text"][data-password="' + atr + '"]').show().focus();
                        $('input[type="password"][data-password="' + atr + '"]').hide();
                        $('a[data-password-button="' + atr + '"]').html('');
                        $('a[data-password-button="' + atr + '"]').html('<i class="fa fa-eye-slash fa-2x" aria-hidden="true"></i>');
                        
                } else {
                    
                        $('input[type="text"][data-password="' + atr + '"]').hide();
                        $('input[type="password"][data-password="' + atr + '"]').show().focus();
                        $('a[data-password-button="' + atr + '"]').html('');
                        $('a[data-password-button="' + atr + '"]').html('<i class="fa fa-eye fa-2x" aria-hidden="true">');
                }          
            });
          


