export class UnitResult<ErrorType>{
    public isSuccess:boolean
    public error?:ErrorType
    constructor(isSuccess:boolean,error?:ErrorType){
        this.isSuccess=isSuccess
        this.error=error
    }
    public static Success<ErrorType>(){
        return new UnitResult<ErrorType>(true)
    }

    public static Failure<ErrorType>(error: ErrorType){
        return new UnitResult<ErrorType>(false, error)
    }
}

export class Result<ValueType,ErrorType>{
    public isSuccess:boolean
    public error?:ErrorType
    public value?:ValueType
    constructor(isSuccess:boolean,error?:ErrorType,value?:ValueType){
        this.isSuccess=isSuccess
        this.error=error
        this.value=value
    }
    public static Success<ValueType, ErrorType>(value: ValueType){
        return new Result<ValueType, ErrorType>(true, undefined, value)
    }

    public static Failure<ValueType, ErrorType>(error: ErrorType){
        return new Result<ValueType, ErrorType>(false, error)
    }

    public static SuccessInPromise<ValueType, ErrorType>(value: ValueType){
        return Promise.resolve(new Result<ValueType, ErrorType>(true, undefined, value))
    }

    public static FailureInPromise<ValueType, ErrorType>(error: ErrorType){
        return Promise.resolve(new Result<ValueType, ErrorType>(false, error))
    }

}