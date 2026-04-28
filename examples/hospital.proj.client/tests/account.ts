import { test, expect,type Page,type Locator } from '@playwright/test';
import LoginPageModel  from './PageModels/LoginPageModel.js';
import { json } from 'stream/consumers';


test('login page renders correctly', async ({ page }) => {
  //Arrange
  const loginPage = new LoginPageModel(page);
  loginPage.goto()
 //Assert
 await loginPage.expectRendered()

});

test('login canceled by Cancel button', async ({ page }) => {
  //Arrange
  await page.goto('/')
  const previousUrl = page.url()
  
  const loginPage = new LoginPageModel(page);
  await loginPage.goto()

  //Act
  await loginPage.cancel()
//Assert
  await expect(page).toHaveURL(previousUrl)

});

test('user switched to register', async ({ page }) => {
  //Arrange
  const loginPage = new LoginPageModel(page);
  loginPage.goto()

  //Act
  await loginPage.register()
 //Assert
 await expect(page).toHaveURL('/account/register')


});

test('login page renders', async ({ page }) => {
  //Arrange
  page.route('https://localhost:7070/api/account/login',async route=>{
    
    await route.fulfill({status:200})
  })
  
  const loginPage = new LoginPageModel(page);
  await loginPage.goto()

  //Act
  await loginPage.login()
 //Assert
 await expect(page).toHaveURL('/')

});