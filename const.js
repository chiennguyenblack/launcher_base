let config;
if (typeof process.env.NODE_ENV != 'undefined' && process.env.NODE_ENV.trim() === 'stage') {
    config = {
        host: 'http://kinggun.net:82',
        debug: false
    }
} else if (typeof process.env.NODE_ENV != 'undefined' && process.env.NODE_ENV.trim() === 'development') {
    config = {
        host: 'http://kinggun.net:82',
    }
} else {
    // Prod mode
    config = {
        host: 'http://kinggun.net:82',
        debug: false
    }
}
module.exports = config;
