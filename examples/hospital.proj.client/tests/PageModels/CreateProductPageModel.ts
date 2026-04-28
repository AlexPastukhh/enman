import {type Page,expect,Locator} from '@playwright/test'
export default class LoginPageModel{
     page: Page;
     productNameInput: Locator;
     descriptionInput: Locator;
     priceInput: Locator;
     imageInput: Locator; 
     createBtn: Locator;
     cancelBtn: Locator;
  
    constructor(page: Page) {
      this.page = page;
      this.productNameInput = page.locator('[data-test="productName"]')
      this.descriptionInput =page.locator('[data-test="description"]')
      this.priceInput = page.locator('[data-test="price"]')
      this.imageInput =page.locator('[data-test="image"]')
      this.createBtn=page.getByRole('button',{name:'create'})
      this.cancelBtn=page.getByRole('button',{name:'cancel'})
  
    }
     
    async goto() {
      await this.page.goto('/product/create');
    }

    async create() {
      await this.productNameInput.fill('Some good product')
      await this.descriptionInput.fill('good@1 productgood  good good product good good product')
      await this.createBtn.click()
    }

    async cancel() {
      await this.cancelBtn.click()
  
    }
    async expectRendered(){
      await expect(this.productNameInput).toBeVisible()
      await expect(this.descriptionInput).toBeVisible()
      await expect(this.createBtn).toBeVisible()
      await expect(this.cancelBtn).toBeVisible()
    }

  }
   
   
  