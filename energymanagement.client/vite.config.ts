import dns from "node:dns";
import fs from "node:fs";
import path from "node:path";
import react from "@vitejs/plugin-react";
import { defineConfig } from "vite";
import svgr from "vite-plugin-svgr";

// Keep dev server on IPv4 localhost so Windows does not prefer https://[::1]:5173.
dns.setDefaultResultOrder("ipv4first");

// https://vite.dev/config/
export default defineConfig({
  plugins: [svgr(), react()],
  server: {
    host: "localhost",
    https: {
      key: fs.readFileSync(path.resolve(__dirname, "certs", "localhost-key.pem")),
      cert: fs.readFileSync(path.resolve(__dirname, "certs", "localhost.pem")),
    },
    port: 5173,
    fs: {
      // allow reading from repo root so ../../Shared works
      allow: [path.resolve(__dirname, "..", "..")],
    },
    proxy: {
      "/api": {
        target: "https://localhost:7250",
        changeOrigin: true,
        secure: false,
      },
    },
  },
});
