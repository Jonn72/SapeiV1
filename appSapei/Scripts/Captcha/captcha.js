
jQuery(document).ready(function ($) {

    // Do the biznizz:
    $('#mc-form').motionCaptcha({
        shapes: ['triangle', 'x', 'rectangle', 'circle', 'check', 'zigzag', 'arrow', 'delete', 'pigtail', 'star']
    });

    // Yep:
    $("input.placeholder").placeholder();
});
 