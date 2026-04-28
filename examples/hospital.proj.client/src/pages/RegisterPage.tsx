import { zodResolver } from "@hookform/resolvers/zod";
import { SubmitHandler, useForm } from "react-hook-form";
import { Link, useNavigate } from "react-router-dom";
import { z } from "zod";
import { fetchWrapper } from "../Utils/fetchWrapper";
import { useRegister } from "../Hooks/useRegister";
import { routes } from "../Utils/routes";

export default function RegisterPage()
{

    const { getRegisterErrors, registerScheme } = useRegister()

    const navigate = useNavigate()

    type RegisterDto = z.infer<typeof registerScheme>

    const {
        register,
        handleSubmit,
        setError,
        formState: { errors, isSubmitting }
    } = useForm<RegisterDto>({
        resolver: zodResolver(registerScheme)
    })

    const OnSubmit: SubmitHandler<RegisterDto> = async (dto) =>
    {
        const response = await fetchWrapper.post(
            '/api/Account/register',
            dto)
        if (!response.ok)
        {
            console.log(response)
            const error = await getRegisterErrors(response)
            
            console.log(error)
            setError(error.propName, {
                message: error.message
            })
            
        } else if (response.ok)
        {
            return navigate(routes.activate)
        }
    }

    return (
        <div className="d-flex justify-content-center align-items-center" style={{ minHeight: "100vh" }}>

            <form onSubmit={handleSubmit(OnSubmit)}
                className="row  justify-content-center 
                flex-column 
                align-items-center"
            >
                <label htmlFor="emailInput"
                >Email
                </label>
                <input className="form-control"
                    type="text"
                    id="emailInput"
                    data-testid="emailInput"
                    {...register("email")}
                />

                {errors.email &&
                    <span className="text-danger">
                        {errors.email.message}
                    </span>
                }


                <label htmlFor="firstNameInput">First Name</label>
                <input className="form-control"
                    type="text"
                    id="firstNameInput"
                    data-testid="firstNameInput"
                    {...register("name.firstName")} />

                {errors.name?.firstName &&
                    <span className="text-danger">
                        {errors.name.firstName.message}
                    </span>
                }


                <label htmlFor="middleNameInput">Middle Name</label>
                <input className="form-control"
                    type="text"
                    id="middleNameInput"
                    data-testid="middleNameInput"
                    {...register("name.middleName")} />

                {errors.name?.middleName &&
                    <span className="text-danger">
                        {errors.name.middleName.message}
                    </span>
                }


                <label htmlFor="lastNameInput">Last Name</label>
                <input className="form-control"
                    type="text"
                    id="lastNameInput"
                    data-testid="lastNameInput"
                    {...register("name.lastName")} />
                {errors.name?.lastName &&
                    <span className="text-danger">
                        {errors.name.lastName.message}
                    </span>
                }

                <label htmlFor="passwordInput">Password</label>
                <input className="form-control"
                    type="text"
                    id="passwordInput"
                    {...register("password")} />

                {errors.password &&
                    <span className="text-danger">
                        {errors.password.message}
                    </span>
                }


                <label htmlFor="passwordConfirmInput">Confirm Password</label>
                <input className="form-control"
                    type="text"
                    id="passwordConfirmInput"
                    {...register("passwordConfirm")} />

                {errors.passwordConfirm &&
                    <span className="text-danger">
                        {errors.passwordConfirm.message}
                    </span>
                }


                <button
                    disabled={isSubmitting}
                    className="btn btn-primary"
                    id="registerBtn"
                >Register
                </button>

                <Link to={'login'}
                    className="btn btn-primary"
                    id="loginPageLink"
                >Login
                </Link>

                <Link to={'/'}
                    className="btn btn-secondary"
                    id="cancelLink"
                >Cancel
                </Link>

                {errors.root &&
                    <span className="text-danger">
                        {errors.root.message}
                    </span>
                }
            </form>
        </div>
    )
}
