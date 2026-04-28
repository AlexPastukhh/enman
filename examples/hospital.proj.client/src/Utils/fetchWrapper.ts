export const fetchWrapper={
    post:call('POST'),
    get:call('GET'),
    put:call('PUT'),
    delete:call('DELETE')
}

function call(method:string){
    return (url:string,body?:object|undefined)=>{
        const requestOptions:RequestInit = {
            method
            ,
            credentials:"include"
        };
        
        const headers:HeadersInit = {}

        // const authHeaders = {
            
        // }
        // if(authHeaders){
        //     Object.assign(headers,authHeaders) 
        // }

        if(body){
            headers['Content-Type'] =
                'application/json'
            requestOptions.body = JSON.stringify(body)

            requestOptions.headers = headers;

        }

        return fetch(url,requestOptions)
    }
}

