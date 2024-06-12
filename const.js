let config;
if (typeof process.env.NODE_ENV != 'undefined' && process.env.NODE_ENV.trim() === 'stage') {
    config = {
        host: 'http://api.gun2008.vn',
        debug: false
    }
} else if (typeof process.env.NODE_ENV != 'undefined' && process.env.NODE_ENV.trim() === 'development') {
    config = {
        host: 'http://api.gun2008.vn',
    }
} else {
    // Prod mode
    config = {
        host: 'http://api.gun2008.vn',
        debug: false
    }
}
module.exports = config;
