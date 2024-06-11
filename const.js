let config;
if (typeof process.env.NODE_ENV != 'undefined' && process.env.NODE_ENV.trim() === 'stage') {
    config = {
        host: 'http://api-v2.gunbactrungnam.com',
        debug: false
    }
} else if (typeof process.env.NODE_ENV != 'undefined' && process.env.NODE_ENV.trim() === 'development') {
    config = {
        host: 'http://api-v2.gunbactrungnam.com',
    }
} else {
    // Prod mode
    config = {
        host: 'http://api-v2.gunbactrungnam.com',
        debug: false
    }
}
module.exports = config;
