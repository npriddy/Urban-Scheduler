
/***********************************************************************
* validateEmail
* Description: Verifies that the email address passed in as valid
* Parameters:  email - the string email
***********************************************************************/
function validateEmail(email)
{
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}

/***********************************************************************
* getQueryString
* Description: Grabs query string beased on string
* Parameters:  key get string
***********************************************************************/
function getQuerystring(key, default_)
{
    default_ = default_ || "";
    key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regex = new RegExp("[\\?&]" + key + "=([^&#]*)", "i");
    var qs = regex.exec(window.location.search);
    return !!qs ? qs[1] : default_;
}

/***********************************************************************
* isMoble
* Description: Checks to see if the user agent is mobile or not
* Parameters:  (none)
***********************************************************************/
function isMobile()
{
    var isIPad = navigator.userAgent.match(/iPad/i) != null;
    return isIPad;
}

/***********************************************************************
* PopupCenter
* Description: Opens the target location and centers popup
* Parameters:  url + width and height
***********************************************************************/
var PopupCenter = (function ()
{
    var defaults = {
        toolbar: 'no', location: 'no', directories: 'no', status: 'yes', menubar: 'no', width: '600', height: '800',
        scrollbars: 'yes', resizable: 'no', copyhistory: 'no', top: (screen.height / 2), left: (screen.width / 2)
    }, opts = [];

    var isMobile = function ()
    {
        var isIPad = navigator.userAgent.match(/iPad/i) != null;
        return isIPad;
    }

    var _run_from_arguments = function (_fn, _window, _arguments)
    {
        _fn = _fn in _window ? _window[_fn] : _fn;
        if (typeof _fn == "function")
        {
            return _fn.apply(_window || window, _arguments);
        }
        return _fn;
    }

    // master popup center function
    var pc = function (url, name, w, h, target, options)
    {
        var opts = [], settings = $.extend(true, options || {}, defaults);
        settings['width'] = w;
        settings['height'] = h;
        settings['top'] = (screen.height / 2) / 2;
        settings['left'] = (screen.width / 2) / 2;

        for (k in settings)
        {
            opts.push(k + "=" + settings[k]);
        }

        if (isMobile())
        {
            alert('mobile');
            window.open(url, '_blank');
            return false;
        }

        // assign target to this function
        PopupCenter.targetWindow = window.open(url, name, opts.join(", "));
        if (!isMobile())
        {
            if (!!PopupCenter.targetWindow && !PopupCenter.targetWindow.closed)
                PopupCenter.targetWindow.focus();
        }

        //PopupCenter.targetWindow.focus();
    }
    pc.RunOnPopup = function (fn)
    {
        // does the popup still exist?
        if (!!this.targetWindow)
        {
            return _run_from_arguments(fn, this.targetWindow, Array.prototype.splice.call(arguments, 2));
        }
    }
    pc.RunOnOpener = function (fn)
    {
        // does the opening window still exist?
        if (!!window.opener)
        {
            return _run_from_arguments(fn, window.opener, Array.prototype.splice.call(arguments, 1));
        }
    }

    return pc;
})();