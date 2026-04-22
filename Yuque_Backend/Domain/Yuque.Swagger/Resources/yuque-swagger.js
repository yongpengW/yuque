var yuque = yuque || {};
(function () {
    yuque.utils = yuque.utils || {};

    yuque.utils.setCookieValue = function (key, value, expireDate, path) {
        var cookieValue = encodeURIComponent(key) + '=';

        if (value) {
            cookieValue = cookieValue + encodeURIComponent(value);
        }

        if (expireDate) {
            cookieValue = cookieValue + '; expires=' + expireDate.toUTCString();
        }

        if (path) {
            cookieValue = cookieValue + '; path=' + path;
        }

        document.cookie = cookieValue;
    };

    yuque.utils.getCookieValue = function (key) {
        var equalities = document.cookie.split('; ');
        for (var i = 0; i < equalities.length; i++) {
            if (!equalities[i]) {
                continue;
            }

            var splitted = equalities[i].split('=');
            if (splitted.length != 2) {
                continue;
            }

            if (decodeURIComponent(splitted[0]) === key) {
                return decodeURIComponent(splitted[1] || '');
            }
        }

        return null;
    };

    yuque.auth = yuque.auth || {};

    yuque.auth.tokenHeaderName = 'Authorization';
    yuque.auth.tokenCookieName = 'Yuque.AuthToken';

    yuque.auth.getToken = function () {
        return yuque.utils.getCookieValue(yuque.auth.tokenCookieName);
    };

    yuque.auth.setToken = function (token, expireDate) {
        console.log('SetToken', token, expireDate);
        yuque.utils.setCookieValue(yuque.auth.tokenCookieName, token, expireDate, '/');
    };

    yuque.auth.clearToken = function () {
        yuque.auth.setToken();
    };

    yuque.auth.requestInterceptor = function (request) {
        var token = yuque.auth.getToken();
        request.headers.Authorization = token;

        return request;
    };

    yuque.swagger = yuque.swagger || {};

    yuque.swagger.openAuthDialog = function (loginCallback) {
        yuque.swagger.closeAuthDialog();

        var authDialog = document.createElement('div');
        authDialog.className = 'dialog-ux';
        authDialog.id = 'yuque-auth-dialog';

        authDialog.innerHTML = `<div class="backdrop-ux"></div>
        <div class="modal-ux">
            <div class="modal-dialog-ux">
                <div class="modal-ux-inner">
                    <div class="modal-ux-header">
                        <h3>接口授权登录</h3>
                        <button type="button" class="close-modal">
                            <svg width="20" height="20">
                                <use href="#close" xlink:href="#close"></use>
                            </svg>
                        </button>
                    </div>
                    <div class="modal-ux-content">
                        <div class="auth-form-wrapper"></div>
                        <div class="auth-btn-wrapper">
                            <button class="btn modal-btn auth btn-done button">取消</button>
                            <button type="submit" class="btn modal-btn auth authorize button">登录</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>`;

        var formWrapper = authDialog.querySelector('.auth-form-wrapper');

        // username
        createInput(formWrapper, 'username', '账号');

        // password
        createInput(formWrapper, 'password', '密码', 'password');

        document.getElementsByClassName('swagger-ui')[1].appendChild(authDialog);

        authDialog.querySelector('.btn-done.modal-btn').onclick = function () {
            yuque.swagger.closeAuthDialog();
        };

        authDialog.querySelector('.authorize.modal-btn').onclick = function () {
            yuque.swagger.login(loginCallback);
        };

        window.addEventListener("keydown", function (event) {
            console.log(event.key, "event.key");
            if (event.key === 'Enter') {
                yuque.swagger.login(loginCallback);
            }
        });
        authDialog.querySelector('.close-modal').onclick = function () {
            yuque.swagger.closeAuthDialog();
        };
    };

    yuque.swagger.closeAuthDialog = function () {
        if (document.getElementById('yuque-auth-dialog')) {
            document.getElementsByClassName('swagger-ui')[1].removeChild(document.getElementById('yuque-auth-dialog'));
        }
    };

    yuque.swagger.login = async function (callback) {
        var data = {
            userName: document.getElementById('username').value,
            // 密码暂时前端通过base64进行转码加密，swagger这里做特殊处理
            password: "swagger" + document.getElementById('password').value,
            platform: 0,
        };

        if (data.userName === "" || data.password === "") {
            alert("账号或密码不能为空");
            return;
        }

        await fetch(`${yuque.host}/api/basic/token/password`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        })
            .then((response) => response.json())
            .then(async (response) => {
                if (response.code === 200) {
                    var expireDate = new Date(response.data.expirationDate);
                    yuque.auth.setToken(response.data.token, expireDate);
                    callback();
                } else {
                    alert(response.message);
                }
            });
    };

    yuque.swagger.logout = function () {
        yuque.auth.clearToken();
    };

    function createInput(container, id, title, type) {
        var wrapper = document.createElement('div');
        wrapper.className = 'form-item';

        var label = document.createElement('label');
        label.innerText = title;
        label.className = 'form-item-label';
        wrapper.appendChild(label);

        var section = document.createElement('section');
        section.className = 'form-item-control';
        wrapper.appendChild(section);

        var input = document.createElement('input');
        input.id = id;
        input.type = type ? type : 'text';
        input.style.width = '100%';
        section.appendChild(input);

        container.appendChild(wrapper);
    }

    // 拦截 SwaggerUIBundle 的赋值，在配置初始化时注入自定义 authorizeBtn 插件。
    // yuque-swagger.js 通过 HeadContent 注入在 <head>，此时 <body> 里的
    // swagger-ui-bundle.js 尚未加载，因此可以通过 defineProperty setter 安全拦截。
    (function () {
        var _original;
        Object.defineProperty(window, 'SwaggerUIBundle', {
            configurable: true,
            get: function () { return _original; },
            set: function (fn) {
                _original = function (config) {
                    function getCssClass() {
                        return (yuque.auth && yuque.auth.getToken && yuque.auth.getToken()) ? 'cancel' : 'authorize';
                    }
                    function getText() {
                        return (yuque.auth && yuque.auth.getToken && yuque.auth.getToken()) ? '退出' : '登录';
                    }

                    config.plugins = (config.plugins || []).concat([function (system) {
                        return {
                            components: {
                                authorizeBtn: function () {
                                    return system.React.createElement(
                                        'div', { className: 'auth-wrapper' },
                                        system.React.createElement('button', {
                                            id: 'authorize',
                                            className: 'btn ' + getCssClass(),
                                            style: { lineHeight: 'normal' },
                                            onClick: function () {
                                                if (yuque.auth.getToken()) {
                                                    yuque.swagger.logout();
                                                    location.reload();
                                                } else {
                                                    yuque.swagger.openAuthDialog(function () { location.reload(); });
                                                }
                                            }
                                        }, getText())
                                    );
                                }
                            }
                        };
                    }]);
                    return fn(config);
                };
                Object.assign(_original, fn);
            }
        });
    })();
})();
