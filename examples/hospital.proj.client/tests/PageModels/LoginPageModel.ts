import {type Page,expect,Locator} from '@playwright/test'
export default class LoginPageModel{
     page: Page;
     usernameInput: Locator;
     passwordInput: Locator; 
     registerBtn: Locator;
     loginBtn: Locator;
     cancelBtn: Locator;
  
    constructor(page: Page) {
      this.page = page;
      this.usernameInput = page.locator('[data-test="username"]')
      this.passwordInput =page.locator('[data-test="password"]')
      this.registerBtn=page.getByRole('button',{name:'register'})
      this.loginBtn=page.getByRole('button',{name:'login'})
      this.cancelBtn=page.getByRole('button',{name:'cancel'})
  
    }
     
    async goto() {
      await this.page.goto('/account/login');
    }

    async login() {
      await this.usernameInput.fill('Username')
      await this.passwordInput.fill('somePassword@1')
      await this.loginBtn.click()
    }
    async register() {
      await this.registerBtn.click()
    }
  
    async cancel() {
      await this.cancelBtn.click()
  
    }
    async expectRendered(){
      await expect(this.usernameInput).toBeVisible()
      await expect(this.passwordInput).toBeVisible()
      await expect(this.loginBtn).toBeVisible()
      await expect(this.registerBtn).toBeVisible()
      await expect(this.cancelBtn).toBeVisible()

    }

  }
   
   
  