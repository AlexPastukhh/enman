type Severity="info"|"warn"|"error";

export class EMLogger{
    private static Log(message:string,severity:Severity="info"){
        switch(severity){
            case "info":
                console.log(message);
                break;
            case "warn":
                console.warn(message);
                break;
            case "error":
                console.error(message);
                break;
        }
    }
    static Info(message:string){
        this.Log(message,"info");
    }
    static Warn(message:string){
        this.Log(message,"warn");
    }
    static Error(message:string){
        this.Log(message,"error");
    }
}

export const LogMsgs={
    FormTests:{
        TimeoutWaitingForCondition:"Timeout waiting for condition",
        FormErrorFound:"Form error found"

    }
}