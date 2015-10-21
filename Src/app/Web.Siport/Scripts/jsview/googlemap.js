var map = null;
var geocoder = null;

function initialize() {


    google.maps.visualRefresh = true;
    var Liverpool = new google.maps.LatLng(-12.12118164118659, -77.02748495872196);

    // These are options that set initial zoom level, where the map is centered globally to start, and the type of map to show
    var mapOptions = {
        zoom: 14,
        center: Liverpool,
        mapTypeId: google.maps.MapTypeId.G_NORMAL_MAP
    };

    // This makes the div with id "map_canvas" a google map
    var map = new google.maps.Map(document.getElementById("map"), mapOptions);

    // This shows adding a simple pin "marker" - this happens to be the Tate Gallery in Liverpool!
    var myLatlng = new google.maps.LatLng(53.40091, -2.994464);

    var marker = new google.maps.Marker({
        position: myLatlng,
        map: map,
        title: 'Tate Gallery'
    });

    // You can make markers different colors...  google it up!
    marker.setIcon('http://maps.google.com/mapfiles/ms/icons/green-dot.png')

    // a sample list of JSON encoded data of places to visit in Liverpool, UK
    // you can either make up a JSON list server side, or call it from a controller using JSONResult
    var data = [
              { "Id": 1, "PlaceName": "Miraflores, Lima", "": "9-5, M-F", "GeoLong": "-12.12118164118659", "GeoLat": "-77.02748495872196" },
              { "Id": 2, "PlaceName": "San Isidro, Lima ", "": "9-1,2-5, M-F", "GeoLong": "-12.098535074465392", "GeoLat": "-77.03517540000001" },
              { "Id": 3, "PlaceName": "La Molina, Lima", "": "9-7, M-F", "GeoLong": "-12.089951731263625", "GeoLat": "-76.92971620000003" },
              { "Id": 4, "PlaceName": "La Victoria, Lima", "": "10-6, M-F", "GeoLong": "-12.074113960903535", "GeoLat": "-77.01586325" }
    ];

    // Using the JQuery "each" selector to iterate through the JSON list and drop marker pins
    $.each(data, function (i, item) {
        var marker = new google.maps.Marker({
            'position': new google.maps.LatLng(item.GeoLong, item.GeoLat),
            'map': map,
            'title': item.PlaceName
        });

        // Make the marker-pin blue!
        marker.setIcon('http://maps.google.com/mapfiles/ms/icons/blue-dot.png')

        // put in some information about each json object - in this case, the opening hours.
        var infowindow = new google.maps.InfoWindow({
            content: "<div class='infoDiv'><h2>" + item.PlaceName + "</h2></div>" //+ "<div><h4>: " + item.OpeningHours + "</h4></div></div>"
        });

        // finally hook up an "OnClick" listener to the map so it pops up out info-window when the marker-pin is clicked!
        google.maps.event.addListener(marker, 'click', function () {
            infowindow.open(map, marker);
        });

    })

    //if (GBrowserIsCompatible()) {
    //    map = new GMap2(document.getElementById("map_canvas"));
    //    map.setCenter(new GLatLng(21.4419, 0), 1);
    //    geocoder = new GClientGeocoder();
    //}
}
function getLatLng(point) {
    var matchll = /\(([-.\d]*), ([-.\d]*)/.exec(point);
    if (matchll) {
        var lat = parseFloat(matchll[1]);
        var lon = parseFloat(matchll[2]);
        lat = lat.toFixed(6);
        lon = lon.toFixed(6);
    }
    else {
        var message = "<b>Error extracting info from</b>:" + point + "";
        var messagRoboGEO = message;
    }
    return new GLatLng(lat, lon);
}
function searchPlace(place) {
    if (geocoder) {
        geocoder.getLatLng(place, function (point) {
            if (!point) {
                alert(place + " not found");
            }
            else {
                var latLng = getLatLng(point);
                var info = "<h3>" + place + "</h3>Latitude: " + latLng.lat() + "  Longitude:" + latLng.lng();
                var marker = new GMarker(point);
                map.addOverlay(marker);
                marker.openInfoWindowHtml(info);
            }
        });
    }
}
