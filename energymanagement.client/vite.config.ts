import fs from 'fs'
import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import svgr from 'vite-plugin-svgr'
import path from 'path'
// https://vite.dev/config/
export default defineConfig({
  plugins: [svgr(), react()],
 server: {
  https: {
      key: fs.readFileSync(path.resolve(__dirname, 'certs', 'localhost-key.pem')),
      cert: fs.readFileSync(path.resolve(__dirname, 'certs', 'localhost.pem')),
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
})
