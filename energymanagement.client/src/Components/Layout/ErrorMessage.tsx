type ErrorMessageProps={message:string};
export const ErrorMessage: React.FC<ErrorMessageProps> = ({message})=>{
    return(
        <div className="errorMessageWrapper">
            <p className="errorMessage">{message}</p>
        </div>
        )
}