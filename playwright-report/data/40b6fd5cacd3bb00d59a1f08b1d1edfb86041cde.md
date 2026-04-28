# Page snapshot

```yaml
- generic [ref=e2]:
  - banner [ref=e3]:
    - heading "Energy Management System" [level=1] [ref=e4]
    - link "Home" [ref=e5] [cursor=pointer]:
      - /url: /
    - link "Register" [ref=e6] [cursor=pointer]:
      - /url: /register
    - link "Login" [ref=e7] [cursor=pointer]:
      - /url: /login
  - main [ref=e8]:
    - generic [ref=e9]:
      - heading "Login" [level=2] [ref=e10]
      - generic [ref=e11]:
        - generic [ref=e12]: Email
        - textbox "Email" [ref=e13]:
          - /placeholder: Enter your email
          - text: email@gmail.com
      - generic [ref=e14]:
        - generic [ref=e15]: Password
        - generic [ref=e16]:
          - textbox "Password" [active] [ref=e17]:
            - /placeholder: Enter your password
            - text: ValidPassword111!
          - button "Show Password" [ref=e18] [cursor=pointer]:
            - img [ref=e19]
      - button "Login" [disabled] [ref=e22]
  - contentinfo [ref=e23]:
    - heading "Energy Management System" [level=1] [ref=e24]
```