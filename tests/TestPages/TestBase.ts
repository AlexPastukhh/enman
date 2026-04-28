import type { Page,Locator } from "@playwright/test";
import { formConst } from "../../energymanagement.client/src/Components/Form/formConst";


export class TestBase {
  protected page: Page;
  constructor(page: Page) {
    this.page = page;
  }
  protected static getErrorsOfInput = async (input: Locator,page:TestPage): Promise<Locator> => {
    const inputId = (await input.getAttribute("id")) as string;
    const errorLabel = formConst.getAriaLabelForError(inputId);
    return page.getByRoleFullName("alert", { name: errorLabel });
  };

  
  
  
}

// type augmentation so TS knows about the method on Page
// declare module '@playwright/test' {
//   interface Page {
//     getByRoleWrap(
//       role: Parameters<Page['getByRole']>[0],
//       options?: Parameters<Page['getByRole']>[1]
//     ): ReturnType<Page['getByRole']>;
//   }
// }

export type TestPage = Page & {
  getByRoleFullName(
    role: Parameters<Page['getByRole']>[0],
    options?: Parameters<Page['getByRole']>[1]
  ): ReturnType<Page['getByRole']>;
};
// attach helper per Page instance (no prototype mutation)
function attachPageHelpers(page: Page) {
  if ((page as TestPage).getByRoleFullName) return;

  (page as TestPage).getByRoleFullName = function (
    this: Page,
    role: Parameters<Page['getByRole']>[0],
    options?: Parameters<Page['getByRole']>[1]
  ) {
    if(options && options.name){
      return this.getByRole(role, {...options,name:new RegExp(`^${options.name}$`)});
    }else{
      return this.getByRole(role, options);
    }
    }

    
}

export const getTestPage=(page:Page):TestPage=>{
  attachPageHelpers(page);
  return page as TestPage;
}

export class SUTFormField extends TestBase{
    private input:Locator;
    private errors:Locator;
    private constructor(page:Page,input:Locator,errors:Locator){
        super(page);
        this.input = input;
        this.errors = errors;
    }
    static Create = async (page:TestPage,inputLabel:string)=>{
        const input = page.getByRoleFullName("textbox",{name:inputLabel});
        const errors = await TestBase.getErrorsOfInput(input,page);
        return new SUTFormField(page,input,errors);
    }
    
    HasErrorsAsync = async () => {
        return await this.errors.isVisible; 
    }
    Fill = async(value:string)=>{
        await this.input.fill(value);
    }
}




  
