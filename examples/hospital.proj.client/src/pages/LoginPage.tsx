import { Link } from "react-router-dom";

export default function LoginPage() {


    return (
        <div className="d-flex justify-content-center align-items-center" style={{ minHeight: "100vh" }}>
            <form className="row  justify-content-center flex-column align-items-center">
                    <label htmlFor="emailInput">Email</label>
                    <input className="form-control" type="text" id="emailInput" />

                    <label htmlFor="passwordInput">Password</label>
                    <input className="form-control" type="text" id="passwordInput" />

                <button type="submit"
                    className="btn btn-primary">Login</button>

                    <Link className="btn btn-primary" to={'/register'}>
                        Register
                    </Link>
                    
                    <Link className="btn btn-secondary" to={'/'}>
                        Cancel
                    </Link>
            </form>
        </div>
    )
}