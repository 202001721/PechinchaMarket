import static com.kms.katalon.core.checkpoint.CheckpointFactory.findCheckpoint
import static com.kms.katalon.core.testcase.TestCaseFactory.findTestCase
import static com.kms.katalon.core.testdata.TestDataFactory.findTestData
import static com.kms.katalon.core.testobject.ObjectRepository.findTestObject
import static com.kms.katalon.core.testobject.ObjectRepository.findWindowsObject
import com.kms.katalon.core.checkpoint.Checkpoint as Checkpoint
import com.kms.katalon.core.cucumber.keyword.CucumberBuiltinKeywords as CucumberKW
import com.kms.katalon.core.mobile.keyword.MobileBuiltInKeywords as Mobile
import com.kms.katalon.core.model.FailureHandling as FailureHandling
import com.kms.katalon.core.testcase.TestCase as TestCase
import com.kms.katalon.core.testdata.TestData as TestData
import com.kms.katalon.core.testng.keyword.TestNGBuiltinKeywords as TestNGKW
import com.kms.katalon.core.testobject.TestObject as TestObject
import com.kms.katalon.core.webservice.keyword.WSBuiltInKeywords as WS
import com.kms.katalon.core.webui.keyword.WebUiBuiltInKeywords as WebUI
import com.kms.katalon.core.windows.keyword.WindowsBuiltinKeywords as Windows
import internal.GlobalVariable as GlobalVariable
import org.openqa.selenium.Keys as Keys

WebUI.openBrowser('')

WebUI.navigateToUrl('https://pechinchamarket.azurewebsites.net/')

WebUI.click(findTestObject('Object Repository/Page_Home Page - PechinchaMarket/a_Register'))

WebUI.click(findTestObject('Object Repository/Page_Register - PechinchaMarket/span_Comerciante_account-type-comerciante-logo'))

def nanoTimeAsString = System.nanoTime().toString()
def email = 'pechincha+' + nanoTimeAsString + '@gmail.com'

WebUI.setText(findTestObject('Object Repository/Page_Registar Comerciante - PechinchaMarket/input_Criar conta como Comerciante_Input.Email'), 
    email)

WebUI.setText(findTestObject('Object Repository/Page_Registar Comerciante - PechinchaMarket/input_Email_Input.UserName'), 
    'Pechincha')

WebUI.click(findTestObject('Object Repository/Page_Registar Comerciante - PechinchaMarket/div_Password'))

WebUI.click(findTestObject('Object Repository/Page_Registar Comerciante - PechinchaMarket/label_Password'))

WebUI.setEncryptedText(findTestObject('Object Repository/Page_Registar Comerciante - PechinchaMarket/input_Nome_Input.Password'), 
    '8MulHJ6VnguuWGcei07mUQ==')

WebUI.setEncryptedText(findTestObject('Object Repository/Page_Registar Comerciante - PechinchaMarket/input_Password_Input.ConfirmPassword'), 
    '8MulHJ6VnguuWGcei07mUQ==')

WebUI.setText(findTestObject('Object Repository/Page_Registar Comerciante - PechinchaMarket/input_Confirme a password_Input.Contact'), 
    '929333292')

WebUI.click(findTestObject('Object Repository/Page_Registar Comerciante - PechinchaMarket/div_Continuar'))

WebUI.click(findTestObject('Object Repository/Page_Registar Comerciante - PechinchaMarket/label_Carregar'))

WebUI.click(findTestObject('Object Repository/Page_Registar Comerciante - PechinchaMarket/div_Nenhum ficheiro escolhido'))

WebUI.click(findTestObject('Object Repository/Page_Registar Comerciante - PechinchaMarket/label_Carregar'))

WebUI.click(findTestObject('Object Repository/Page_Registar Comerciante - PechinchaMarket/button_Registar'))

WebUI.click(findTestObject('Object Repository/Page_WaitForConfirmation - PechinchaMarket/a_Login'))

WebUI.closeBrowser()

