export const routes={
    home:'/',

    register:'/account/register',
    login: '/account/login',
    activate: '/account/activate',
    activated:'/account/activated',
    _proxyBase:'https://localhost:7142',
    _spaBase:'https://localhost:5173',
    getProxyUrl(path:string){
        return this._proxyBase+path
    },

    getSpaUrl(path:string){
      return this._spaBase+path  
    },

    getHomeProxyUrl():string{
        return this.getProxyUrl(this.home)
    },

    getHomeSpaUrl():string{
        return this.getSpaUrl(this.home)
    },

    getLoginSpaUrl():string{
        return this.getSpaUrl(this.login)
    },

    getLoginProxyUrl():string{
        return this.getProxyUrl(this.login)
    },

    getRegisterSpaUrl():string{
        return this.getSpaUrl(this.register)
    },

    getRegisterProxyUrl():string{
        return this.getProxyUrl(this.register)
    },
}
    
