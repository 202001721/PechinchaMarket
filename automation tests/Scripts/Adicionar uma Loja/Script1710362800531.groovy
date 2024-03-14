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

WebUI.click(findTestObject('Object Repository/Page_Home Page - PechinchaMarket/a_Login'))

WebUI.click(findTestObject('Object Repository/Page_Login - PechinchaMarket/div_Email'))

WebUI.setText(findTestObject('Object Repository/Page_Login - PechinchaMarket/input_Login_Input.Email'), 'lidl@gmail.com')

WebUI.setEncryptedText(findTestObject('Object Repository/Page_Login - PechinchaMarket/input_Email_Input.Password'), '8MulHJ6VnguuWGcei07mUQ==')

WebUI.click(findTestObject('Object Repository/Page_Login - PechinchaMarket/button_Password_password-hidden'))

WebUI.click(findTestObject('Object Repository/Page_Login - PechinchaMarket/button_Password_password-hidden'))

WebUI.click(findTestObject('Object Repository/Page_Login - PechinchaMarket/button_Login'))

WebUI.click(findTestObject('Object Repository/Page_Home Page - PechinchaMarket/a_Lojas'))

WebUI.click(findTestObject('Object Repository/Page_Index - PechinchaMarket/a_Adicionar Loja'))

def uniqueNumber = System.currentTimeMillis()

def address = 'Rua Vice Almirante ' + uniqueNumber

WebUI.setText(findTestObject('Object Repository/Page_Create - PechinchaMarket/input_Morada_Address'), address)

WebUI.setText(findTestObject('Object Repository/Page_Create - PechinchaMarket/Page_Create - PechinchaMarket/input_Horrio de Abertura_OpeningTime'), 
    '20:50')

WebUI.setText(findTestObject('Object Repository/Page_Create - PechinchaMarket/Page_Create - PechinchaMarket/input_Horrio de Fecho_ClosingTime'), 
    '23:00')

WebUI.click(findTestObject('Page_Create - PechinchaMarket/input_Horrio de Fecho_button'))

