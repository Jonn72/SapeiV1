function OcultaPanel(objeto) {
    var $BOX_PANEL = $(objeto).closest('.x_panel'),
        $ICON = $(objeto).find('i'),
        $BOX_CONTENT = $BOX_PANEL.find('.x_content');

    // fix for some div with hardcoded fix class
    if ($BOX_PANEL.attr('style')) {
        $BOX_CONTENT.slideToggle(200, function () {
            $BOX_PANEL.removeAttr('style');
        });
    } else {
        $BOX_CONTENT.slideToggle(200);
        $BOX_PANEL.css('height', 'auto');
    }

    $ICON.toggleClass('fa-chevron-up fa-chevron-down');
}

function CierraPanel(objeto) {
    var $BOX_PANEL = $(objeto).closest('.x_panel');

    $BOX_PANEL.remove();
}