import { logoutClientAccount as postLogoutClientAccount } from "../../../../shared/api/authApi";

export const logoutClientAccount = (): Promise<void> => postLogoutClientAccount();
