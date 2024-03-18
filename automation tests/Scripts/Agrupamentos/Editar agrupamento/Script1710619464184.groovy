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

WebUI.setText(findTestObject('Object Repository/Page_Login - PechinchaMarket/input_Login_Input.Email'), 'clientepecmk@gmail.com')

WebUI.click(findTestObject('Object Repository/Page_Login - PechinchaMarket/main_Precisa de ajuda                      _508d9a'))

WebUI.setText(findTestObject('Object Repository/Page_Login - PechinchaMarket/input_Login_Input.Email'), 'clientepecmk@gmail.com')

WebUI.click(findTestObject('Object Repository/Page_Login - PechinchaMarket/label_Password'))

WebUI.setEncryptedText(findTestObject('Object Repository/Page_Login - PechinchaMarket/input_Email_Input.Password'), 'VyCYYIKrGZmyN9bkhIcWmA==')

WebUI.click(findTestObject('Object Repository/Page_Login - PechinchaMarket/button_Login'))

WebUI.click(findTestObject('Object Repository/Page_Home Page - PechinchaMarket/span_Perfil                                _6a042e'))

WebUI.click(findTestObject('Object Repository/Page_Home Page - PechinchaMarket/a_Perfil'))

WebUI.click(findTestObject('Object Repository/Page_Profil - PechinchaMarket/a_Agrupamentos'))

WebUI.click(findTestObject('Object Repository/Page_Agrupamentos - PechinchaMarket/span_Agrupamento da famlia'))

WebUI.click(findTestObject('Object Repository/Page_Agrupamentos - PechinchaMarket/div_Editar'))

WebUI.doubleClick(findTestObject('Object Repository/Page_Agrupamentos - PechinchaMarket/input_Agrupamento da famlia_nome'))

WebUI.setText(findTestObject('Object Repository/Page_Agrupamentos - PechinchaMarket/input_Agrupamento da famlia_nome'), 
    'Agrupamento da família')

WebUI.click(findTestObject('Object Repository/Page_Agrupamentos - PechinchaMarket/input_Nome_pechincha-button main-button-color'))

WebUI.click(findTestObject('Object Repository/Page_Agrupamentos - PechinchaMarket/span_Agrupamento da famlia'))

WebUI.click(findTestObject('Object Repository/Page_Agrupamentos - PechinchaMarket/div_Adicionar Lista'))

WebUI.click(findTestObject('Object Repository/Page_Agrupamentos - PechinchaMarket/label_Lista de compras'))

WebUI.click(findTestObject('Object Repository/Page_Agrupamentos - PechinchaMarket/button_Salvar'))

WebUI.click(findTestObject('Object Repository/Page_Agrupamentos - PechinchaMarket/span_Agrupamento da famlia'))

WebUI.delay(1)

WebUI.closeBrowser()
