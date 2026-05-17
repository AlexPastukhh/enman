import { makeCurrentDefaultButtonConst } from "./makeCurrentDefaultButtonConst";
import "./makeCurrentDefaultButton.css";

type MakeCurrentDefaultButtonProps = {
  isPending?: boolean;
  errorMessage?: string | null;
  onClick: () => void;
};

export const MakeCurrentDefaultButton = ({
  isPending = false,
  errorMessage = null,
  onClick,
}: MakeCurrentDefaultButtonProps) => (
  <div className="makeCurrentDefaultAction">
    <button type="button" disabled={isPending} onClick={onClick}>
      {isPending
        ? makeCurrentDefaultButtonConst.pendingLabel
        : makeCurrentDefaultButtonConst.actionLabel}
    </button>
    {errorMessage && (
      <p className="makeCurrentDefaultAction__error" role="alert">
        {errorMessage}
      </p>
    )}
  </div>
);
