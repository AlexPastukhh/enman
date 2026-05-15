type Props = React.HtmlHTMLAttributes<HTMLHeadingElement>

export const FormTitle:React.FC<Props> =({children, className, ...rest})=>{
    return(
        <h2 className={`formTitle ${className || ""}`} {...rest}>{children}</h2>
    )
}