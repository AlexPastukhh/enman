import { test, expect } from '@playwright/test';
import { routes } from '../src/Utils/routes'
import { RegisterPageModel } from './PageModels/RegisterPageModel';

test.describe('Home page',()=>{

    test.beforeEach(async({page})=>{
        await page.goto(
            routes.getHomeProxyUrl()
        );
    })

    test('Home page renders', async ({ page }) => {

        const registerLink = await page.getByRole(
            'link', { name: 'Register' })

        const loginLink = await page.getByRole(
            'link', { name: 'Login' })

        await expect(registerLink).toBeVisible()
        await expect(loginLink).toBeVisible()

    });

    test('Click on link and redirect to register', async ({ page }) => {
        

        const registerLink = await page.getByRole(
            'link', { name: 'Register' })

        await registerLink.click()

        await expect(page).toHaveURL(
            routes.getRegisterSpaUrl())
    });

    test('Click on link and redirect to login', async ({ page }) => {
        

        const registerLink = await page.getByRole(
            'link', { name: 'Login' })

        await registerLink.click()

        await expect(page).toHaveURL(
            routes.getLoginSpaUrl())
    });
});

test.describe('Register page',()=>{
    test.beforeEach(async({page})=>{
        await page.goto(
            routes.getRegisterProxyUrl()
        )
    })

    test('Registers user successfully',async({page})=>{
        const RegisterPM = new RegisterPageModel(page)

        await RegisterPM.FillFormCorrectly()

        await RegisterPM.clickRegister()

        await expect(page).toHaveURL(
            routes.getHomeSpaUrl()
        )

    });
});
    