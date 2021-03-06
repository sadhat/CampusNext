﻿campusNextApp.service('EnvConfig', function() {
        /**
 * You can have as many environments as you like in here
 * just make sure the host matches up to your hostname including port
 */
        var _environments = {
                local: {
                    host: 'localhost:40000',
                    config: {
                        /**
             * Add any config properties you want in here for this environment
             */
                        apiroot: 'http://localhost:50000/'
                    }
                },
                prod: {
                    host: 'flopmvp.com',
                    config: {
                        /**
             * Add any config properties you want in here for this environment
             */
                        apiroot: 'https://flopmvp.com/'
                    }
                }
            },
            _environment;

        return {
            getEnvironment: function() {
                var host = window.location.host;

                if (_environment) {
                    return _environment;
                }

                for (var environment in _environments) {
                    if (typeof _environments[environment].host && _environments[environment].host == host) {
                        _environment = environment;
                        return _environment;
                    }
                }

                return null;
            },
            get: function(property) {
                return _environments[this.getEnvironment()].config[property];
            }
        }
    }
);
