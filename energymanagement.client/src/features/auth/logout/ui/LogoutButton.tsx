import { useLogoutAction } from "../model/useLogoutAction";

type LogoutButtonProps = {
  className?: string;
  label: string;
  pendingLabel: string;
};

export const LogoutButton = ({
  className,
  label,
  pendingLabel,
}: LogoutButtonProps) => {
  const { logout, isPending, errorMessage } = useLogoutAction();

  return (
    <div className="header__logout">
      <button
        className={`button-hollow ${className ?? ""}`.trim()}
        type="button"
        onClick={() => void logout()}
        disabled={isPending}
      >
        {isPending ? pendingLabel : label}
      </button>
      {errorMessage && (
        <p className="header__logout-error" role="alert">
          {errorMessage}
        </p>
      )}
    </div>
  );
};
