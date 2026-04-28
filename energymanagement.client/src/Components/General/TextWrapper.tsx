type TextWrapperProps = Omit<
  React.HTMLAttributes<HTMLParagraphElement>,
  "className"
> & {
  addClassName?: string;
};
export const TextWrapper: React.FC<TextWrapperProps> = ({
  children,
  ...rest
}) => {
  return (
    <>
      <p className={`textWithLink ${rest.addClassName || ""}`} {...rest}>
        {children}
      </p>
    </>
  );
};
