import { createContext, useCallback, useContext, useState } from "react";

type PageErrorContextValue = {
  pageError: string | undefined;
  setPageError: React.Dispatch<React.SetStateAction<string | undefined>>;
  removePageError: () => void;
};

const pageErrorContext = createContext<PageErrorContextValue | undefined>(
  undefined,
);

export const PageErrorProvider = ({
  children,
}: {
  children: React.ReactNode;
}) => {
  const [pageError, setPageError] = useState<string | undefined>(undefined);
  const removePageError = useCallback(
    () => setPageError(undefined),
    [setPageError],
  );

  return (
    <pageErrorContext.Provider
      value={{ pageError, setPageError, removePageError }}
    >
      {children}
    </pageErrorContext.Provider>
  );
};

export const usePageError = () => {
  const context = useContext(pageErrorContext);
  if (!context) {
    throw new Error("usePageError must be used within a PageErrorProvider");
  }

  return { ...context };
};

