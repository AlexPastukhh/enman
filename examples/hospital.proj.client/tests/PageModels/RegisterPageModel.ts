 import { expect, Locator, Page } from "@playwright/test";

export class RegisterPageModel{
    page:Page
    registerBtn:Locator
    loginPageLink:Locator
    cancelLink:Locator

    emailInput:Locator

    firstNameInput:Locator
    middleNameInput:Locator
    lastNameInput:Locator

    passwordInput:Locator
    passwordConfirmInput:Locator
     constructor(page:Page){
        this.registerBtn=page.getByTestId(
            'registerBtn')
        this.loginPageLink=page.getByTestId(
            'loginPageLink')
        this.emailInput=page.getByTestId(
            'emailInput')
        this.firstNameInput=page.getByTestId(
            'firstNameInput')
        this.middleNameInput=page.getByTestId(
            'middleNameInput')
        this.lastNameInput=page.getByTestId(
            'lastNameInput')
        this.passwordInput=page.getByTestId(
            'passwordInput')
        this.passwordConfirmInput=page.getByTestId(
            'passwordConfirmInput')
        this.cancelLink=page.getByTestId(
            'cancelLink')
    }

    async FillFormCorrectly(){

        await this.emailInput.fill('kelton.ledner@ethereal.email ')

        await this.firstNameInput.fill('First')
        await this.middleNameInput.fill('Middle')
        await this.lastNameInput.fill('Last')

        await this.passwordInput.fill('fseg3fe@1fd#')
        await this.passwordConfirmInput.fill('fseg3fe@1fd#')
    }

    async clickRegister(){
        await this.registerBtn.click()
    }

    async expectRendersCorrectly(){
        await expect(this.registerBtn).toBeVisible
        await expect(this.loginPageLink).toBeVisible
        await expect(this.cancelLink).toBeVisible

        await expect(this.emailInput).toBeVisible

        await expect(this.firstNameInput).toBeVisible
        await expect(this.middleNameInput).toBeVisible
        await expect(this.lastNameInput).toBeVisible

        await expect(this.passwordInput)
            .toBeVisible
        await expect(this.passwordConfirmInput)
            .toBeVisible
    }

    
}