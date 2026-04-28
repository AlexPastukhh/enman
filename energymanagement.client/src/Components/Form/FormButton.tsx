type ButtonProps = 
    Omit<React.ButtonHTMLAttributes<HTMLButtonElement>, 'type'> 
    & {type:"button" | "submit" | "reset"};


export const FormButton:React.FC<ButtonProps> =(
    {children,
    type,
    className,
    ...rest})=>{
    return(
        <button className={`button-primary ${className || ""}`} type={type} {...rest}>{children}</button>
    )
}