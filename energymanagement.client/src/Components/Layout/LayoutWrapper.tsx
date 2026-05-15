import { useEffect } from "react";
import { usePageError } from "../../shared/errors/pageErrorContext";
import { ErrorMessage } from "./ErrorMessage";
import { Footer } from "./Footer"
import { Header } from "./Header"
import { useLocation } from "react-router-dom";

export const LayoutWrapper = ({ children }: { children: React.ReactNode }) => {
    const {pageError,removePageError} = usePageError();
    const {pathname}= useLocation();
    useEffect (()=>{
        removePageError();
    }, [removePageError, pathname]);

    return (
 <>
    <Header/>
    {children}
    {pageError && <ErrorMessage message={pageError}/>}
    <Footer/>
    </>   
)
}
