import { useRef } from "react"
import type { Path, UseFormRegister, UseFormTrigger } from "react-hook-form"
import type { TDto } from "../Utils/FormSchemas"

type FormRegisterType=UseFormRegister<TDto>
// export type FormRegisterDebounce= ReturnType<FormRegisterType>


// type of register(name) result
type RegisterReturn = Omit<ReturnType<FormRegisterType>, 'onChange'> & {
  onChange: (e: React.ChangeEvent<HTMLInputElement>) => void
}

// signature of the register helper returned by the hook
export type FormRegister = (name: Path<TDto>) => RegisterReturn

// hook return type


export const useFormRegisterDebounce =(
        registerFunc:FormRegisterType,
        triggerFunc:UseFormTrigger<TDto>)=>{
    const timeOutRefs = useRef<Record<string,ReturnType<typeof setTimeout>>>({})
    const register =(
        name: Path<TDto>)=>{
            const registerField = registerFunc(name)
            return {
                ...registerField,
                onChange: (e:React.ChangeEvent<HTMLInputElement>)=>{
                    registerField.onChange(e)
                    if (timeOutRefs.current[name]) {
                        clearTimeout(timeOutRefs.current[name])
                    }
    
                    timeOutRefs.current[name] = setTimeout(()=>{
                        
                        triggerFunc(name)
                    },500)
                }
                
            } 
        }
    return { register }
}