const PROXY_CONFIG = [
    {
        context: [
            "/api",
            "/odata"
        ],
        target: "https://localhost:44325",
        secure: false
    }
]
module.exports = PROXY_CONFIG;
