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

WebUI.navigateToUrl('https://pechinchamarket.azurewebsites.net/Identity/Account/Login')

WebUI.setText(findTestObject('Object Repository/Page_Login - PechinchaMarket/input_ou_Input.Email'), 'comerciantepecmk@gmail.com')

WebUI.setEncryptedText(findTestObject('Object Repository/Page_Login - PechinchaMarket/input_Email_Input.Password'), 'VyCYYIKrGZmyN9bkhIcWmA==')

WebUI.click(findTestObject('Object Repository/Page_Login - PechinchaMarket/button_Login'))

WebUI.click(findTestObject('Object Repository/Page_Home Page - PechinchaMarket/a_Produtos'))

WebUI.click(findTestObject('Object Repository/Page_Index - PechinchaMarket/a_Produtos Pendentes_img-edit'))

WebUI.setText(findTestObject('Object Repository/Page_Edit - PechinchaMarket/input__price'), '2')

WebUI.click(findTestObject('Object Repository/Page_Edit - PechinchaMarket/input_Valor_shops'))

WebUI.click(findTestObject('Object Repository/Page_Edit - PechinchaMarket/input_Unit_discounts'))

WebUI.setText(findTestObject('Object Repository/Page_Edit - PechinchaMarket/input_Valor_valor'), '25')

WebUI.click(findTestObject('Object Repository/Page_Edit - PechinchaMarket/input_Ilustrativo_btngerar'))

WebUI.closeBrowser()

