
type PutOptions = Omit<RequestInit,"method" & "body" & "headers">;
const postRequest = async(
    path:string, 
    body: object,
    init?: PutOptions,
    headers?: HeadersInit)=>{

    const response = await fetch(path,
        {...init,
        method:"POST",
        headers: headers? headers :{ "Content-Type": "application/json" },
        body: JSON.stringify(body),})
    return response;
}

type GetOptions = Omit<RequestInit,"method">;
const getRequest = async(
    path:string, 
    init?: GetOptions)=>{

    const response = await fetch(path,
        {...init,
        method:"GET"})
    return response;
}


export const fetchWrapper={
    post:postRequest,
    get:getRequest,
    put:()=>{},
    delete:()=>{},
}