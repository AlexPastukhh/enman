type FormFieldProps = Omit<React.HtmlHTMLAttributes<HTMLDivElement>, "className"> &{
        addClassName?:string;
    }
    
export const FormGroup:React.FC<FormFieldProps> = 
    ({children,
    addClassName,
    ...rest}) => {
    return (
        <div {...rest} className={`formGroup + ${addClassName || ""}`}>
            {children}
        </div>
    )
}