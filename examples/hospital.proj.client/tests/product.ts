import { test, expect,type Page,type Locator } from '@playwright/test';
import CreateProductPageModel  from './PageModels/CreateProductPageModel.js';
import { json } from 'stream/consumers';

test('page renders correctly', async ({ page }) => {
  //Arrange
  const createPage = new CreateProductPageModel(page);
  await createPage.goto()
 //Assert
 await createPage.expectRendered()

});

test('login canceled by Cancel button', async ({ page }) => {
  //Arrange
  await page.goto('/')
  const previousUrl = page.url()
  const createPage = new CreateProductPageModel(page);
  await createPage.goto()

  //Act
  await createPage.cancel()
//Assert
  await expect(page).toHaveURL(previousUrl)

});

test('product creates and redirect to returnUrl', async ({ page }) => {
  //Arrange
  page.route('https://localhost:7070/api/product/create',async route=>{
    await route.fulfill({status:200})
  })
  await page.goto('/')
  const previousUrl = page.url()
  const createPage = new CreateProductPageModel(page);
  await createPage.goto()
  //Act
  await createPage.create()
 //Assert
  await expect(page).toHaveURL(previousUrl)
  const alert= page.getByRole('alert')
  await expect(alert).toHaveClass('success')
});