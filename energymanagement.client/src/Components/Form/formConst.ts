import type { MultipleFieldErrors } from "react-hook-form"

export class  formConst{
    static rootErrorId="formRootErrors"
    static labelForRootErrors="formRootErrorsLabel"

    static ariaLabelShowPwd="Show Password"
    static ariaLabelHidePwd="Hide Password"
    static errorsAriaLabelPrefix="Errors For "
    static getAriaLabelForError=(inputId:string)=>{
        return this.errorsAriaLabelPrefix+inputId
    }
    static  getMultipleErrorsStr=(types: MultipleFieldErrors | undefined)=>{
        const messages = Object.values(types??{})
        if(messages.length===0){
            throw new Error("No error messages found")
        }
        return messages.join(" ")
    }
}