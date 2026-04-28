import { z } from "zod"


export const useRegister = () => {

    const registerScheme = z.object({
        name: z.object({
            firstName: z.string()
                .max(50, '50 characters max')
                .min(1,'First name is required'),
            middleName: z.string()
                .max(50, '50 characters max')
                .min(1,'Middle name is required'),
            lastName: z.string()
                .max(50, '50 characters max')
                .min(1,'Last name is required')
        }),
        email: z.string()
        .max(100, '100 characters max')
        .min(1,'Email is required')
        .regex(/^(.+)@(.+)$/,'Invalid format of email'),
        password:z.string()
            .min(12,'Min length 12')
            .max(100, '100 characters max')
            .min(1,'Password is required'),
        passwordConfirm: z.string()
            .min(1,'Field is required')
            
    }).refine(dto => dto.password == dto.passwordConfirm, {
        message: 'Passwords doesn\'t match',
        path: ["passwordConfirm"]
    })


        
    type RegisterError={
        propName:'email',
        message:string
    }

    const getRegisterErrors=
        async (response:Response)
        :Promise<RegisterError>=>
        {
            //const data= await response.json()


            //if (Object.getOwnPropertyNames(data).includes("code")) {
            //    var error = { propName: "email", message: errors[data.code] }
            //    errors.push(data)
            //}

            const error:RegisterError = { propName: 'email', message:'The email is already registered.'}
       
        
        return error
    }

    

    return {registerScheme,getRegisterErrors }

}


