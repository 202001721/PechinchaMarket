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

WebUI.setEncryptedText(findTestObject('Object Repository/Page_Login - PechinchaMarket/input_Email_Input.Password'), 'VyCYYIKrGZmyN9bkhIcWmA==')

WebUI.click(findTestObject('Object Repository/Page_Login - PechinchaMarket/button_Login'))

WebUI.click(findTestObject('Object Repository/Page_Home Page - PechinchaMarket/a_Logout_cursor-pointer small-image nav-lin_58ce0e'))

WebUI.click(findTestObject('Object Repository/Page_Index - PechinchaMarket/a_As minhas listas_create'))

WebUI.setText(findTestObject('Object Repository/Page_Create - PechinchaMarket/input_Nome_Name'), 'Lista para remover')

WebUI.click(findTestObject('Object Repository/Page_Create - PechinchaMarket/input_Nome_btn btn-primary'))

WebUI.click(findTestObject('Object Repository/Page_Index - PechinchaMarket/span_Perfil                                _4f1926'))

WebUI.click(findTestObject('Object Repository/Page_Index - PechinchaMarket/span_Perfil'))

WebUI.click(findTestObject('Object Repository/Page_Profil - PechinchaMarket/a_Agrupamentos'))

WebUI.click(findTestObject('Object Repository/Page_Agrupamentos - PechinchaMarket/span_Agrupamento 1'))

WebUI.click(findTestObject('Object Repository/Page_Agrupamentos - PechinchaMarket/div_Adicionar Lista'))

WebUI.click(findTestObject('Object Repository/Page_Agrupamentos - PechinchaMarket/input_Lista de Produtos_listaId'))

WebUI.click(findTestObject('Object Repository/Page_Agrupamentos - PechinchaMarket/button_Salvar'))

WebUI.click(findTestObject('Object Repository/Page_Agrupamentos - PechinchaMarket/span_Agrupamento 1'))

WebUI.click(findTestObject('Object Repository/Page_Agrupamentos - PechinchaMarket/input_btn btn-danger'))

WebUI.click(findTestObject('Object Repository/Page_Agrupamentos - PechinchaMarket/input_Horrio de Fecho_button'))

WebUI.click(findTestObject('Object Repository/Page_Agrupamentos - PechinchaMarket/a_PechinchaMarket_pechincha-navbar-option c_ab4045'))

WebUI.click(findTestObject('Object Repository/Page_Index - PechinchaMarket/a_As minhas listas_bin'))

WebUI.click(findTestObject('Object Repository/Page_Delete - PechinchaMarket/input__btn btn-danger'))

WebUI.closeBrowser()

