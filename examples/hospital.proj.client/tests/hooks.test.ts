import { it, expect, describe } from 'vitest'
import {useRegister} from '../src/Hooks/useRegister'
describe('useRegister', () => {
    it('', () => {
        const {registerScheme}=useRegister()
        type FormFields = z.infer<typeof registerScheme>;
        
    })
})