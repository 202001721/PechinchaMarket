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

WebUI.click(findTestObject('Object Repository/Page_Register - PechinchaMarket/button_Cliente'))

var(email = (('cliente+' + System.nanoTime()) + '@gmail.com'))

WebUI.setText(findTestObject('Object Repository/Page_Registar - PechinchaMarket/input_Criar conta como Cliente_Input.Email'), 
    email)

WebUI.setText(findTestObject('Object Repository/Page_Registar - PechinchaMarket/input_Email_Input.UserName'), 'Cliente')

WebUI.setText(findTestObject('Object Repository/Page_Registar - PechinchaMarket/input_Nome_Input.Localizacao'), 'Setúbal')

WebUI.click(findTestObject('Object Repository/Page_Registar - PechinchaMarket/div_Email                                  _c11387'))

WebUI.setEncryptedText(findTestObject('Object Repository/Page_Registar - PechinchaMarket/input_Localizao_Input.Password'), 
    'VyCYYIKrGZmyN9bkhIcWmA==')

WebUI.setEncryptedText(findTestObject('Object Repository/Page_Registar - PechinchaMarket/input_Password_Input.ConfirmPassword'), 
    'VyCYYIKrGZmyN9bkhIcWmA==')

WebUI.click(findTestObject('Object Repository/Page_Registar - PechinchaMarket/div_Continuar'))

WebUI.click(findTestObject('Object Repository/Page_Registar - PechinchaMarket/label_Doces'))

WebUI.click(findTestObject('Object Repository/Page_Registar - PechinchaMarket/label_Snacks'))

WebUI.click(findTestObject('Object Repository/Page_Registar - PechinchaMarket/label_Pastelaria'))

WebUI.click(findTestObject('Object Repository/Page_Registar - PechinchaMarket/button_Registar'))

WebUI.closeBrowser()

