# Mini Vite IPv4 + CSS fix

From repo root, apply with:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\mini-vite-ipv4-css-fix.zip" -DestinationPath "." -Force
```

Then run:

```powershell
npm --prefix .\energymanagement.client run dev
```

Open:

```text
https://localhost:5173
```

What this changes:

- `energymanagement.client/vite.config.ts`
  - forces Node/Vite localhost resolution to IPv4 first;
  - sets Vite dev server host to `localhost`;
  - keeps existing HTTPS cert/proxy setup.

- `energymanagement.client/src/main.tsx`
  - imports `general.css` and `layout.css` in addition to `index.css`.
