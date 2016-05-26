function clickedWithinHeader(event) {
    var target = event.currentTarget;
    var hotspot = null;
    var hotspots = target.getElementsByClassName("modal-header");
    if (hotspots.length > 0) {
        hotspot = hotspots.item(0);
    }
    if (hotspot !== null) { // since the header occupies the full width across the top no need to check X.
        // Note that this assumes the header is on the top, which should be a safe assumption
        var within = (event.clientY - target.offsetTop <= hotspot.offsetHeight);
        return within;
    } else {
        return true;
    }
}

(function () {
    angular
        .module('app')
        .directive('draggable', function ($document) {
            return function (scope, element) {
                var startX = 0, startY = 0, x = 0, y = 0;
                element = angular.element(document.getElementsByClassName("modal-dialog"));

                function mousemove(event) {
                    y = event.screenY - startY;
                    x = event.screenX - startX;
                    element.css({
                        top: y + 'px',
                        left: x + 'px'
                    });
                }

                function mouseup() {
                    $document.unbind('mousemove', mousemove);
                    $document.unbind('mouseup', mouseup);
                }

                element.on('mousedown', function (event) {
                    if (!clickedWithinHeader(event)) {
                        return;
                    }
                    event.preventDefault();
                    startX = event.screenX - x;
                    startY = event.screenY - y;
                    $document.on('mousemove', mousemove);
                    $document.on('mouseup', mouseup);
                });
            };
        });
})();