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

WebUI.setText(findTestObject('Object Repository/Page_Login - PechinchaMarket/input_ou_Input.Email'), 'comerciantepecmk@gmail.com')

WebUI.setEncryptedText(findTestObject('Object Repository/Page_Login - PechinchaMarket/input_Email_Input.Password'), 'VyCYYIKrGZmyN9bkhIcWmA==')

WebUI.click(findTestObject('Object Repository/Page_Login - PechinchaMarket/button_Login'))

WebUI.click(findTestObject('Object Repository/Page_Home Page - PechinchaMarket/a_Produtos'))

WebUI.click(findTestObject('Object Repository/Page_Index - PechinchaMarket/a_Produtos Pendentes_floating-button'))

WebUI.click(findTestObject('Object Repository/Page_Index - PechinchaMarket/a_Criar um produto'))

WebUI.setText(findTestObject('Object Repository/Page_Create - PechinchaMarket/input_Nome_Name'), 'Batatas')

WebUI.setText(findTestObject('Object Repository/Page_Create - PechinchaMarket/input_Marca_Brand'), 'Lays')

WebUI.setText(findTestObject('Object Repository/Page_Create - PechinchaMarket/input_Preo()_price'), '1.50')

WebUI.selectOptionByValue(findTestObject('Object Repository/Page_Create - PechinchaMarket/select_Escolha uma unidade                 _2a5e59'), 
    '0', true)

WebUI.selectOptionByValue(findTestObject('Object Repository/Page_Create - PechinchaMarket/select_Escolha uma categoria               _8ff80a'), 
    '11', true)

WebUI.click(findTestObject('Object Repository/Page_Create - PechinchaMarket/input_Categoria_shops'))

WebUI.click(findTestObject('Object Repository/Page_Create - PechinchaMarket/label_Adicionar Foto'))

WebUI.click(findTestObject('Object Repository/Page_Create - PechinchaMarket/input_Horrio de Fecho_button'))

WebUI.closeBrowser()

