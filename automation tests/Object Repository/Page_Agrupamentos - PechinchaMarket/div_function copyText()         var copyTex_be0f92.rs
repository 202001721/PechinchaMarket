<?xml version="1.0" encoding="UTF-8"?>
<WebElementEntity>
   <description></description>
   <name>div_function copyText()         var copyTex_be0f92</name>
   <tag></tag>
   <elementGuidId>51fbe41f-09b3-44d0-abf6-79393d2e5583</elementGuidId>
   <selectorCollection>
      <entry>
         <key>XPATH</key>
         <value>//div[@id='content-container']</value>
      </entry>
      <entry>
         <key>CSS</key>
         <value>#content-container</value>
      </entry>
   </selectorCollection>
   <selectorMethod>XPATH</selectorMethod>
   <smartLocatorCollection>
      <entry>
         <key>SMART_LOCATOR</key>
         <value>internal:text=&quot;Agrupamentos Membro[s] removido[s] com sucesso! Criar Agrupamento Entrar Agrupam&quot;i</value>
      </entry>
   </smartLocatorCollection>
   <smartLocatorEnabled>false</smartLocatorEnabled>
   <useRalativeImagePath>true</useRalativeImagePath>
   <webElementProperties>
      <isSelected>false</isSelected>
      <matchCondition>equals</matchCondition>
      <name>tag</name>
      <type>Main</type>
      <value>div</value>
      <webElementGuid>73f1526f-d117-420d-83c5-b147a85d2447</webElementGuid>
   </webElementProperties>
   <webElementProperties>
      <isSelected>false</isSelected>
      <matchCondition>equals</matchCondition>
      <name>class</name>
      <type>Main</type>
      <value>col-md-9</value>
      <webElementGuid>dabda29a-4b62-48d6-848c-a867ae1c9a41</webElementGuid>
   </webElementProperties>
   <webElementProperties>
      <isSelected>true</isSelected>
      <matchCondition>equals</matchCondition>
      <name>id</name>
      <type>Main</type>
      <value>content-container</value>
      <webElementGuid>df428bc6-aa3f-4bbc-9d11-c2371e234183</webElementGuid>
   </webElementProperties>
   <webElementProperties>
      <isSelected>true</isSelected>
      <matchCondition>equals</matchCondition>
      <name>text</name>
      <type>Main</type>
      <value>
                            



    function copyText() {
        var copyText = document.getElementById(&quot;codigo&quot;);
        console.log(copyText);
        copyText.select();
        copyText.setSelectionRange(0, 99999);
        document.execCommand(&quot;copy&quot;);
    }

    function colapse(div) {
        div.children[0].classList.toggle(&quot;text-indent&quot;);
        div.children[1].classList.toggle(&quot;active&quot;);
        var hiddenContent = div.nextElementSibling;
        hiddenContent.classList.toggle(&quot;off-screen&quot;);
    }

    function changeNameEditableField(button) {
        var form = button.parentNode.parentNode;
        var elemtToHide = button.parentNode.children[0];

        var inputs = form.querySelectorAll('input');
        inputs.forEach(function (input) {
            if (input.id === 'name-input') {
                input.disabled = !input.disabled;
            }
        });

        var elements = document.querySelectorAll('.authentication-input-text-div');
        elements.forEach(function (element) {

            if (element.querySelector('span.text-danger')) {
                if (element.querySelector('span.text-danger').textContent === '') {
                    element.style.height = element.children[0].getBoundingClientRect().height + 'px';
                }
            }
        });

        elemtToHide.classList.toggle('off-screen');

        if (elemtToHide.classList.contains('off-screen')) {
            button.textContent = &quot;Editar&quot;;
        } else {
            button.textContent = &quot;Cancelar&quot;;
        }
    }

    function chooseListToAdd(text) {
        document.body.classList.toggle('no-overflow');
        chooseimage = document.getElementById(&quot;add-list-&quot; + text);
        chooseimage.classList.toggle('display-none');
    }

    function enterCode() {
        document.body.classList.toggle('no-overflow');
        chooseimage = document.getElementById(&quot;enter-agrupamento&quot;);
        chooseimage.classList.toggle('display-none');
    }

    function chooseMemberToAdd(text) {
        document.body.classList.toggle('no-overflow');
        chooseimage = document.getElementById(&quot;add-member-&quot; + text);
        chooseimage.classList.toggle('display-none');
    }

    function searchPerson(text, agrupamentoId) { 
        var agrupamento = document.getElementById('add-member-to-' + agrupamentoId);

        function searchInElements(element) {
            if (element.textContent.toLowerCase() === text.toLowerCase() &amp;&amp; text !== '') {
                return true;
            }

            for (var i = 0; i &lt; element.children.length; i++) {
                var result = searchInElements(element.children[i]);
                if (result) {
                    return result;
                }
            }
            return false;
        }

        var card = null;
        for (var i = 0; i &lt; agrupamento.children.length; i++){
            if (!agrupamento.children[i].classList.contains('display-none')) { 
                agrupamento.children[i].classList.add('display-none');
            }
            var found = searchInElements(agrupamento.children[i]);
            if (found) { 
                card = agrupamento.children[i];
                break;
            }
        }

        if (card) { 
            card.classList.remove('display-none');
        }
    }

    function selectEdit(permissionBtn){

        var section = permissionBtn.parentNode.parentNode.parentNode

        var checkboxes = Array.from(section.querySelectorAll(&quot;.checkbox&quot;));
        var permissionUpdateBtn = section.querySelector(&quot;#permissionUpdateBtn&quot;);


        for(checkbox of checkboxes){
            if (checkbox.checked) {
                checkbox.parentNode.parentNode.querySelector(&quot;.editPermisions&quot;).style.display = &quot;block&quot;;
            }
        }

        permissionBtn.style.display = &quot;none&quot;;
        permissionUpdateBtn.style.display = &quot;block&quot;;
    }

    function checkTheBox(checkbox,idAgrupamento){
        
        var section = checkbox.parentNode.parentNode.parentNode.parentNode.parentNode
        var variavel = &quot;#formRemove_&quot; + idAgrupamento;
        console.log(variavel);
        var checkboxes = Array.from(section.querySelectorAll(&quot;.checkbox&quot;));

        var permissionUpdateBtn = section.querySelector(&quot;#permissionBtn&quot;);
        var removeBtn = section.querySelector(&quot;#formRemove_&quot;+idAgrupamento);
        var addBtn = section.querySelector(&quot;#addBtn&quot;);

        permissionUpdateBtn.style.display = &quot;none&quot;;
        removeBtn.style.display = &quot;none&quot;;
        addBtn.style.display = &quot;block&quot;;

        for (checkbox of checkboxes) {
            if (checkbox.checked) {

                var permissionUpdateBtn = section.querySelector(&quot;#permissionBtn&quot;);
                var removeBtn = section.querySelector(&quot;#formRemove_&quot;+idAgrupamento);

                permissionUpdateBtn.style.display = &quot;block&quot;;
                removeBtn.style.display = &quot;block&quot;;
                addBtn.style.display = &quot;none&quot;;
            }
        }
    }

    document.addEventListener('DOMContentLoaded', function () {
        const specificDiv = document.getElementById('mensagem');

        function displayMessage(statusMessage) {
            const statusElement = document.createElement('div');
            statusElement.classList.add('alert', 'alert-success', 'status-message');
            statusElement.textContent = statusMessage;

            specificDiv.innerHTML = ''; 
            specificDiv.appendChild(statusElement);

            const displayTime = 3000;
            setTimeout(() => {
                statusElement.style.display = 'none';
            }, displayTime);
        }

        const statusMessage = 'Membro[s] removido[s] com sucesso!';

        if (statusMessage) {
            displayMessage(statusMessage);
        }
    });
    





Agrupamentos
Membro[s] removido[s] com sucesso!

    
        Criar Agrupamento
    
    
        Entrar Agrupamento
    



    
        
            Entrar num Agrupamento
            
        
        
            
                Código
                
            
            
                Entrar
            
        
    



        
            
                Agrupamento:P 
                
            

            
                
                    
                       
                            
                            Copiar Código
                        
                        
                            Eliminar Agrupamento
                        
                    
                    
                        
                            
                                Eliminar Agrupamento
                                
                            

                            
                                Tem certeza que deseja eliminar este agrupamento?
                                
                                    Agrupamento:P   
                                

                                
                                    

                                        

                                             Eliminar Agrupamento

                                        
                                    

                                
                            
                        

                    

                
                    
                        
                        
                            
                            Nome
                            
                        
                        
                            
                                Renomear
                           
                        
                    
                

                
                
                    
                        Listas
                            

                                Adicionar Lista
                                

                                    

                                
                            
                       
                    
                    
                    
                        
                        
                    

                    
                        
                            
                                Adicionar uma Lista
                                
                             
                            
                           
                            
                                
                                Lista de Produtos
                                
                                            
                                                
                                                Lista de Sexta-feira
                                            
                                
                                
                                    Salvar
                                
                            
                        
                    
                

                
                
                    
                


                
                    
                        Membros

                            


                                
                                
                                

                                
                                    
                                

                            
                      
                    
                    
                    
                        
                                            Administradores
                                        Cliente:P
                                            Leitores
                                            
                                            
                                                    
                                                Luis
                                            
                                            
                                                Selecionar Permissão:
                                                
                                                    Leitor
                                                    Editor
                                                    Leitor
                                                
                                            
                                        
                        
                    

                    
                        
                            
                                Adicionar um Membro
                                
                            
                            
                                Pesquise um membro
                                
                                    
                                    Copiar Código
                                
                                
                                    
                                        
                                            
                                            
                                        
                                    
                                    
                                        
                                                    
                                                        
                                                            
                                                            Rodrigo
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            BlitzGB
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            i3ggg
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Matilde
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            luis.goias.1999@gmail.com
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Afonso
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            &lt;h1>Lucas&lt;/h1>
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            ccroyale34@gmail.com
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Cid
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Alexandre
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Duarte
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            yonaxiy
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            xegoron
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            jacinto
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            André Lisboa
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            marta
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Andreia
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Carlos
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Felipe T. Peres
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            testeuser1
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            joao
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Janaina
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            jana.bruno@gmail.com
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            vanessa venites
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Inês Pereira
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Eunice do Carmo
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Nihen
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Rui
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            22
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Util
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Util
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Util
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Automacao
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Automacao
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Automacao
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                        
                                    
                                
                                
                                    Ok
                                
                            
                        
                    
                
            
            
        
        
            
                Agrupamento 1
                
            

            
                

                
                    
                        
                        
                            
                            Nome
                            
                        
                        
                            
                           
                        
                    
                

                
                
                    
                        Listas
                            

                                Adicionar Lista
                                

                                    

                                
                            
                       
                    
                    
                    
                        
                        
                    

                    
                        
                            
                                Adicionar uma Lista
                                
                             
                            
                           
                            
                                
                                Lista de Produtos
                                
                                            
                                                
                                                Lista de Sexta-feira
                                            
                                
                                
                                    Salvar
                                
                            
                        
                    
                

                
                
                    
                


                
                    
                        Membros

                      
                    
                    
                    
                        
                                            Administradores
                                        Luis
                                            Editores
                                        
                                            
                                                
                                                Cliente:P
                                            
                                            
                                                Selecionar Permissão:
                                                
                                                    Editor
                                                    Editor
                                                    Leitor
                                                
                                            
                                        
                        
                    

                    
                        
                            
                                Adicionar um Membro
                                
                            
                            
                                Pesquise um membro
                                
                                    
                                    Copiar Código
                                
                                
                                    
                                        
                                            
                                            
                                        
                                    
                                    
                                        
                                                    
                                                        
                                                            
                                                            Rodrigo
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            BlitzGB
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            i3ggg
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Matilde
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            luis.goias.1999@gmail.com
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Afonso
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            &lt;h1>Lucas&lt;/h1>
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            ccroyale34@gmail.com
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Cid
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Alexandre
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Duarte
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            yonaxiy
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            xegoron
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            jacinto
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            André Lisboa
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            marta
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Andreia
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Carlos
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Felipe T. Peres
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            testeuser1
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            joao
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Janaina
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            jana.bruno@gmail.com
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            vanessa venites
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Inês Pereira
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Eunice do Carmo
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Nihen
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Rui
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            22
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Util
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Util
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Util
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Automacao
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Automacao
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Automacao
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                        
                                    
                                
                                
                                    Ok
                                
                            
                        
                    
                
            
            
        





    document.addEventListener(&quot;DOMContentLoaded&quot;,
        tooltipmsg(&quot;Nesta página, poderá gerir, criar e participar em grupos onde poderá partilhar as suas listas com os membros do grupo.&quot;,&quot;&quot;,&quot;&quot;)
    );

    function collapse(coll) {
        coll.querySelector(&quot;.colapsable-button&quot;).classList.toggle(&quot;active&quot;);
        var content = coll.nextElementSibling;
        if (content.style.maxHeight) {
            content.style.maxHeight = null;
        } else {
            console.log(content.scrollHeight)
            content.style.maxHeight = content.scrollHeight + &quot;px&quot;;
        }
    }

    function removerMembro(){
        document.getElementById(&quot;hideremove&quot;).style.display = &quot;none&quot;;
        document.getElementById(&quot;showremove&quot;).style.display = &quot;inherit&quot;;
        document.getElementById(&quot;showboxes&quot;).style.display = &quot;inherit&quot;;


    }

    function changeNameEditableField(button) {
        var form = button.parentNode.parentNode;
        var elemtToHide = button.parentNode.children[0];

        var inputs = form.querySelectorAll('input');
        inputs.forEach(function (input) {
            if (input.id === 'name-input') {
                input.disabled = !input.disabled;
            }
        });

        var elements = document.querySelectorAll('.authentication-input-text-div');
        elements.forEach(function (element) {

            if (element.querySelector('span.text-danger')) {
                if (element.querySelector('span.text-danger').textContent === '') {
                    element.style.height = element.children[0].getBoundingClientRect().height + 'px';
                }
            }
        });

        elemtToHide.classList.toggle('off-screen');

        if (elemtToHide.classList.contains('off-screen')) {
            button.textContent = &quot;Editar&quot;;
        } else {
            button.textContent = &quot;Cancelar&quot;;
        }
    }

    function chooseListToAdd(text){ 
        document.body.classList.toggle('no-overflow');
        chooseimage = document.getElementById(&quot;add-list-&quot;+text);
        console.log(text);
        console.log(&quot;add-list-&quot; + text);
        console.log(chooseimage);

        chooseimage.classList.toggle('display-none');
    }

    function deletePopup(text) {
        document.body.classList.toggle('no-overflow');
        chooseimage = document.getElementById(&quot;delete-popup-&quot; + text);
        chooseimage.classList.toggle('display-none');
    }




                        </value>
      <webElementGuid>b6f13dca-fb31-4439-9a36-80e31ac51a62</webElementGuid>
   </webElementProperties>
   <webElementProperties>
      <isSelected>false</isSelected>
      <matchCondition>equals</matchCondition>
      <name>xpath</name>
      <type>Main</type>
      <value>id(&quot;content-container&quot;)</value>
      <webElementGuid>0433e7c0-4ca8-4a5a-9030-1dafba064a88</webElementGuid>
   </webElementProperties>
   <webElementXpaths>
      <isSelected>true</isSelected>
      <matchCondition>equals</matchCondition>
      <name>xpath:attributes</name>
      <type>Main</type>
      <value>//div[@id='content-container']</value>
      <webElementGuid>b86d22af-7f42-4c06-b953-654f58e3f10a</webElementGuid>
   </webElementXpaths>
   <webElementXpaths>
      <isSelected>false</isSelected>
      <matchCondition>equals</matchCondition>
      <name>xpath:neighbor</name>
      <type>Main</type>
      <value>(.//*[normalize-space(text()) and normalize-space(.)='Agrupamentos'])[2]/following::div[1]</value>
      <webElementGuid>c6b8587f-60c3-4050-96fc-a5eee93bdf24</webElementGuid>
   </webElementXpaths>
   <webElementXpaths>
      <isSelected>false</isSelected>
      <matchCondition>equals</matchCondition>
      <name>xpath:neighbor</name>
      <type>Main</type>
      <value>(.//*[normalize-space(text()) and normalize-space(.)='Perfil'])[2]/following::div[1]</value>
      <webElementGuid>6d43deca-9b6e-4d2d-94ff-87f359b00e75</webElementGuid>
   </webElementXpaths>
   <webElementXpaths>
      <isSelected>false</isSelected>
      <matchCondition>equals</matchCondition>
      <name>xpath:position</name>
      <type>Main</type>
      <value>//section/div/div[2]</value>
      <webElementGuid>feb61e17-def6-44d2-a05f-7ea606f802f8</webElementGuid>
   </webElementXpaths>
   <webElementXpaths>
      <isSelected>false</isSelected>
      <matchCondition>equals</matchCondition>
      <name>xpath:customAttributes</name>
      <type>Main</type>
      <value>//div[@id = 'content-container' and (text() = concat(&quot;
                            



    function copyText() {
        var copyText = document.getElementById(&quot;codigo&quot;);
        console.log(copyText);
        copyText.select();
        copyText.setSelectionRange(0, 99999);
        document.execCommand(&quot;copy&quot;);
    }

    function colapse(div) {
        div.children[0].classList.toggle(&quot;text-indent&quot;);
        div.children[1].classList.toggle(&quot;active&quot;);
        var hiddenContent = div.nextElementSibling;
        hiddenContent.classList.toggle(&quot;off-screen&quot;);
    }

    function changeNameEditableField(button) {
        var form = button.parentNode.parentNode;
        var elemtToHide = button.parentNode.children[0];

        var inputs = form.querySelectorAll(&quot; , &quot;'&quot; , &quot;input&quot; , &quot;'&quot; , &quot;);
        inputs.forEach(function (input) {
            if (input.id === &quot; , &quot;'&quot; , &quot;name-input&quot; , &quot;'&quot; , &quot;) {
                input.disabled = !input.disabled;
            }
        });

        var elements = document.querySelectorAll(&quot; , &quot;'&quot; , &quot;.authentication-input-text-div&quot; , &quot;'&quot; , &quot;);
        elements.forEach(function (element) {

            if (element.querySelector(&quot; , &quot;'&quot; , &quot;span.text-danger&quot; , &quot;'&quot; , &quot;)) {
                if (element.querySelector(&quot; , &quot;'&quot; , &quot;span.text-danger&quot; , &quot;'&quot; , &quot;).textContent === &quot; , &quot;'&quot; , &quot;&quot; , &quot;'&quot; , &quot;) {
                    element.style.height = element.children[0].getBoundingClientRect().height + &quot; , &quot;'&quot; , &quot;px&quot; , &quot;'&quot; , &quot;;
                }
            }
        });

        elemtToHide.classList.toggle(&quot; , &quot;'&quot; , &quot;off-screen&quot; , &quot;'&quot; , &quot;);

        if (elemtToHide.classList.contains(&quot; , &quot;'&quot; , &quot;off-screen&quot; , &quot;'&quot; , &quot;)) {
            button.textContent = &quot;Editar&quot;;
        } else {
            button.textContent = &quot;Cancelar&quot;;
        }
    }

    function chooseListToAdd(text) {
        document.body.classList.toggle(&quot; , &quot;'&quot; , &quot;no-overflow&quot; , &quot;'&quot; , &quot;);
        chooseimage = document.getElementById(&quot;add-list-&quot; + text);
        chooseimage.classList.toggle(&quot; , &quot;'&quot; , &quot;display-none&quot; , &quot;'&quot; , &quot;);
    }

    function enterCode() {
        document.body.classList.toggle(&quot; , &quot;'&quot; , &quot;no-overflow&quot; , &quot;'&quot; , &quot;);
        chooseimage = document.getElementById(&quot;enter-agrupamento&quot;);
        chooseimage.classList.toggle(&quot; , &quot;'&quot; , &quot;display-none&quot; , &quot;'&quot; , &quot;);
    }

    function chooseMemberToAdd(text) {
        document.body.classList.toggle(&quot; , &quot;'&quot; , &quot;no-overflow&quot; , &quot;'&quot; , &quot;);
        chooseimage = document.getElementById(&quot;add-member-&quot; + text);
        chooseimage.classList.toggle(&quot; , &quot;'&quot; , &quot;display-none&quot; , &quot;'&quot; , &quot;);
    }

    function searchPerson(text, agrupamentoId) { 
        var agrupamento = document.getElementById(&quot; , &quot;'&quot; , &quot;add-member-to-&quot; , &quot;'&quot; , &quot; + agrupamentoId);

        function searchInElements(element) {
            if (element.textContent.toLowerCase() === text.toLowerCase() &amp;&amp; text !== &quot; , &quot;'&quot; , &quot;&quot; , &quot;'&quot; , &quot;) {
                return true;
            }

            for (var i = 0; i &lt; element.children.length; i++) {
                var result = searchInElements(element.children[i]);
                if (result) {
                    return result;
                }
            }
            return false;
        }

        var card = null;
        for (var i = 0; i &lt; agrupamento.children.length; i++){
            if (!agrupamento.children[i].classList.contains(&quot; , &quot;'&quot; , &quot;display-none&quot; , &quot;'&quot; , &quot;)) { 
                agrupamento.children[i].classList.add(&quot; , &quot;'&quot; , &quot;display-none&quot; , &quot;'&quot; , &quot;);
            }
            var found = searchInElements(agrupamento.children[i]);
            if (found) { 
                card = agrupamento.children[i];
                break;
            }
        }

        if (card) { 
            card.classList.remove(&quot; , &quot;'&quot; , &quot;display-none&quot; , &quot;'&quot; , &quot;);
        }
    }

    function selectEdit(permissionBtn){

        var section = permissionBtn.parentNode.parentNode.parentNode

        var checkboxes = Array.from(section.querySelectorAll(&quot;.checkbox&quot;));
        var permissionUpdateBtn = section.querySelector(&quot;#permissionUpdateBtn&quot;);


        for(checkbox of checkboxes){
            if (checkbox.checked) {
                checkbox.parentNode.parentNode.querySelector(&quot;.editPermisions&quot;).style.display = &quot;block&quot;;
            }
        }

        permissionBtn.style.display = &quot;none&quot;;
        permissionUpdateBtn.style.display = &quot;block&quot;;
    }

    function checkTheBox(checkbox,idAgrupamento){
        
        var section = checkbox.parentNode.parentNode.parentNode.parentNode.parentNode
        var variavel = &quot;#formRemove_&quot; + idAgrupamento;
        console.log(variavel);
        var checkboxes = Array.from(section.querySelectorAll(&quot;.checkbox&quot;));

        var permissionUpdateBtn = section.querySelector(&quot;#permissionBtn&quot;);
        var removeBtn = section.querySelector(&quot;#formRemove_&quot;+idAgrupamento);
        var addBtn = section.querySelector(&quot;#addBtn&quot;);

        permissionUpdateBtn.style.display = &quot;none&quot;;
        removeBtn.style.display = &quot;none&quot;;
        addBtn.style.display = &quot;block&quot;;

        for (checkbox of checkboxes) {
            if (checkbox.checked) {

                var permissionUpdateBtn = section.querySelector(&quot;#permissionBtn&quot;);
                var removeBtn = section.querySelector(&quot;#formRemove_&quot;+idAgrupamento);

                permissionUpdateBtn.style.display = &quot;block&quot;;
                removeBtn.style.display = &quot;block&quot;;
                addBtn.style.display = &quot;none&quot;;
            }
        }
    }

    document.addEventListener(&quot; , &quot;'&quot; , &quot;DOMContentLoaded&quot; , &quot;'&quot; , &quot;, function () {
        const specificDiv = document.getElementById(&quot; , &quot;'&quot; , &quot;mensagem&quot; , &quot;'&quot; , &quot;);

        function displayMessage(statusMessage) {
            const statusElement = document.createElement(&quot; , &quot;'&quot; , &quot;div&quot; , &quot;'&quot; , &quot;);
            statusElement.classList.add(&quot; , &quot;'&quot; , &quot;alert&quot; , &quot;'&quot; , &quot;, &quot; , &quot;'&quot; , &quot;alert-success&quot; , &quot;'&quot; , &quot;, &quot; , &quot;'&quot; , &quot;status-message&quot; , &quot;'&quot; , &quot;);
            statusElement.textContent = statusMessage;

            specificDiv.innerHTML = &quot; , &quot;'&quot; , &quot;&quot; , &quot;'&quot; , &quot;; 
            specificDiv.appendChild(statusElement);

            const displayTime = 3000;
            setTimeout(() => {
                statusElement.style.display = &quot; , &quot;'&quot; , &quot;none&quot; , &quot;'&quot; , &quot;;
            }, displayTime);
        }

        const statusMessage = &quot; , &quot;'&quot; , &quot;Membro[s] removido[s] com sucesso!&quot; , &quot;'&quot; , &quot;;

        if (statusMessage) {
            displayMessage(statusMessage);
        }
    });
    





Agrupamentos
Membro[s] removido[s] com sucesso!

    
        Criar Agrupamento
    
    
        Entrar Agrupamento
    



    
        
            Entrar num Agrupamento
            
        
        
            
                Código
                
            
            
                Entrar
            
        
    



        
            
                Agrupamento:P 
                
            

            
                
                    
                       
                            
                            Copiar Código
                        
                        
                            Eliminar Agrupamento
                        
                    
                    
                        
                            
                                Eliminar Agrupamento
                                
                            

                            
                                Tem certeza que deseja eliminar este agrupamento?
                                
                                    Agrupamento:P   
                                

                                
                                    

                                        

                                             Eliminar Agrupamento

                                        
                                    

                                
                            
                        

                    

                
                    
                        
                        
                            
                            Nome
                            
                        
                        
                            
                                Renomear
                           
                        
                    
                

                
                
                    
                        Listas
                            

                                Adicionar Lista
                                

                                    

                                
                            
                       
                    
                    
                    
                        
                        
                    

                    
                        
                            
                                Adicionar uma Lista
                                
                             
                            
                           
                            
                                
                                Lista de Produtos
                                
                                            
                                                
                                                Lista de Sexta-feira
                                            
                                
                                
                                    Salvar
                                
                            
                        
                    
                

                
                
                    
                


                
                    
                        Membros

                            


                                
                                
                                

                                
                                    
                                

                            
                      
                    
                    
                    
                        
                                            Administradores
                                        Cliente:P
                                            Leitores
                                            
                                            
                                                    
                                                Luis
                                            
                                            
                                                Selecionar Permissão:
                                                
                                                    Leitor
                                                    Editor
                                                    Leitor
                                                
                                            
                                        
                        
                    

                    
                        
                            
                                Adicionar um Membro
                                
                            
                            
                                Pesquise um membro
                                
                                    
                                    Copiar Código
                                
                                
                                    
                                        
                                            
                                            
                                        
                                    
                                    
                                        
                                                    
                                                        
                                                            
                                                            Rodrigo
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            BlitzGB
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            i3ggg
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Matilde
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            luis.goias.1999@gmail.com
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Afonso
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            &lt;h1>Lucas&lt;/h1>
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            ccroyale34@gmail.com
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Cid
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Alexandre
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Duarte
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            yonaxiy
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            xegoron
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            jacinto
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            André Lisboa
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            marta
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Andreia
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Carlos
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Felipe T. Peres
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            testeuser1
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            joao
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Janaina
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            jana.bruno@gmail.com
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            vanessa venites
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Inês Pereira
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Eunice do Carmo
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Nihen
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Rui
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            22
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Util
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Util
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Util
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Automacao
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Automacao
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Automacao
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                        
                                    
                                
                                
                                    Ok
                                
                            
                        
                    
                
            
            
        
        
            
                Agrupamento 1
                
            

            
                

                
                    
                        
                        
                            
                            Nome
                            
                        
                        
                            
                           
                        
                    
                

                
                
                    
                        Listas
                            

                                Adicionar Lista
                                

                                    

                                
                            
                       
                    
                    
                    
                        
                        
                    

                    
                        
                            
                                Adicionar uma Lista
                                
                             
                            
                           
                            
                                
                                Lista de Produtos
                                
                                            
                                                
                                                Lista de Sexta-feira
                                            
                                
                                
                                    Salvar
                                
                            
                        
                    
                

                
                
                    
                


                
                    
                        Membros

                      
                    
                    
                    
                        
                                            Administradores
                                        Luis
                                            Editores
                                        
                                            
                                                
                                                Cliente:P
                                            
                                            
                                                Selecionar Permissão:
                                                
                                                    Editor
                                                    Editor
                                                    Leitor
                                                
                                            
                                        
                        
                    

                    
                        
                            
                                Adicionar um Membro
                                
                            
                            
                                Pesquise um membro
                                
                                    
                                    Copiar Código
                                
                                
                                    
                                        
                                            
                                            
                                        
                                    
                                    
                                        
                                                    
                                                        
                                                            
                                                            Rodrigo
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            BlitzGB
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            i3ggg
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Matilde
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            luis.goias.1999@gmail.com
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Afonso
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            &lt;h1>Lucas&lt;/h1>
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            ccroyale34@gmail.com
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Cid
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Alexandre
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Duarte
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            yonaxiy
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            xegoron
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            jacinto
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            André Lisboa
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            marta
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Andreia
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Carlos
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Felipe T. Peres
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            testeuser1
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            joao
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Janaina
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            jana.bruno@gmail.com
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            vanessa venites
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Inês Pereira
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Eunice do Carmo
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Nihen
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Rui
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            22
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Util
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Util
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Util
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Automacao
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Automacao
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Automacao
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                        
                                    
                                
                                
                                    Ok
                                
                            
                        
                    
                
            
            
        





    document.addEventListener(&quot;DOMContentLoaded&quot;,
        tooltipmsg(&quot;Nesta página, poderá gerir, criar e participar em grupos onde poderá partilhar as suas listas com os membros do grupo.&quot;,&quot;&quot;,&quot;&quot;)
    );

    function collapse(coll) {
        coll.querySelector(&quot;.colapsable-button&quot;).classList.toggle(&quot;active&quot;);
        var content = coll.nextElementSibling;
        if (content.style.maxHeight) {
            content.style.maxHeight = null;
        } else {
            console.log(content.scrollHeight)
            content.style.maxHeight = content.scrollHeight + &quot;px&quot;;
        }
    }

    function removerMembro(){
        document.getElementById(&quot;hideremove&quot;).style.display = &quot;none&quot;;
        document.getElementById(&quot;showremove&quot;).style.display = &quot;inherit&quot;;
        document.getElementById(&quot;showboxes&quot;).style.display = &quot;inherit&quot;;


    }

    function changeNameEditableField(button) {
        var form = button.parentNode.parentNode;
        var elemtToHide = button.parentNode.children[0];

        var inputs = form.querySelectorAll(&quot; , &quot;'&quot; , &quot;input&quot; , &quot;'&quot; , &quot;);
        inputs.forEach(function (input) {
            if (input.id === &quot; , &quot;'&quot; , &quot;name-input&quot; , &quot;'&quot; , &quot;) {
                input.disabled = !input.disabled;
            }
        });

        var elements = document.querySelectorAll(&quot; , &quot;'&quot; , &quot;.authentication-input-text-div&quot; , &quot;'&quot; , &quot;);
        elements.forEach(function (element) {

            if (element.querySelector(&quot; , &quot;'&quot; , &quot;span.text-danger&quot; , &quot;'&quot; , &quot;)) {
                if (element.querySelector(&quot; , &quot;'&quot; , &quot;span.text-danger&quot; , &quot;'&quot; , &quot;).textContent === &quot; , &quot;'&quot; , &quot;&quot; , &quot;'&quot; , &quot;) {
                    element.style.height = element.children[0].getBoundingClientRect().height + &quot; , &quot;'&quot; , &quot;px&quot; , &quot;'&quot; , &quot;;
                }
            }
        });

        elemtToHide.classList.toggle(&quot; , &quot;'&quot; , &quot;off-screen&quot; , &quot;'&quot; , &quot;);

        if (elemtToHide.classList.contains(&quot; , &quot;'&quot; , &quot;off-screen&quot; , &quot;'&quot; , &quot;)) {
            button.textContent = &quot;Editar&quot;;
        } else {
            button.textContent = &quot;Cancelar&quot;;
        }
    }

    function chooseListToAdd(text){ 
        document.body.classList.toggle(&quot; , &quot;'&quot; , &quot;no-overflow&quot; , &quot;'&quot; , &quot;);
        chooseimage = document.getElementById(&quot;add-list-&quot;+text);
        console.log(text);
        console.log(&quot;add-list-&quot; + text);
        console.log(chooseimage);

        chooseimage.classList.toggle(&quot; , &quot;'&quot; , &quot;display-none&quot; , &quot;'&quot; , &quot;);
    }

    function deletePopup(text) {
        document.body.classList.toggle(&quot; , &quot;'&quot; , &quot;no-overflow&quot; , &quot;'&quot; , &quot;);
        chooseimage = document.getElementById(&quot;delete-popup-&quot; + text);
        chooseimage.classList.toggle(&quot; , &quot;'&quot; , &quot;display-none&quot; , &quot;'&quot; , &quot;);
    }




                        &quot;) or . = concat(&quot;
                            



    function copyText() {
        var copyText = document.getElementById(&quot;codigo&quot;);
        console.log(copyText);
        copyText.select();
        copyText.setSelectionRange(0, 99999);
        document.execCommand(&quot;copy&quot;);
    }

    function colapse(div) {
        div.children[0].classList.toggle(&quot;text-indent&quot;);
        div.children[1].classList.toggle(&quot;active&quot;);
        var hiddenContent = div.nextElementSibling;
        hiddenContent.classList.toggle(&quot;off-screen&quot;);
    }

    function changeNameEditableField(button) {
        var form = button.parentNode.parentNode;
        var elemtToHide = button.parentNode.children[0];

        var inputs = form.querySelectorAll(&quot; , &quot;'&quot; , &quot;input&quot; , &quot;'&quot; , &quot;);
        inputs.forEach(function (input) {
            if (input.id === &quot; , &quot;'&quot; , &quot;name-input&quot; , &quot;'&quot; , &quot;) {
                input.disabled = !input.disabled;
            }
        });

        var elements = document.querySelectorAll(&quot; , &quot;'&quot; , &quot;.authentication-input-text-div&quot; , &quot;'&quot; , &quot;);
        elements.forEach(function (element) {

            if (element.querySelector(&quot; , &quot;'&quot; , &quot;span.text-danger&quot; , &quot;'&quot; , &quot;)) {
                if (element.querySelector(&quot; , &quot;'&quot; , &quot;span.text-danger&quot; , &quot;'&quot; , &quot;).textContent === &quot; , &quot;'&quot; , &quot;&quot; , &quot;'&quot; , &quot;) {
                    element.style.height = element.children[0].getBoundingClientRect().height + &quot; , &quot;'&quot; , &quot;px&quot; , &quot;'&quot; , &quot;;
                }
            }
        });

        elemtToHide.classList.toggle(&quot; , &quot;'&quot; , &quot;off-screen&quot; , &quot;'&quot; , &quot;);

        if (elemtToHide.classList.contains(&quot; , &quot;'&quot; , &quot;off-screen&quot; , &quot;'&quot; , &quot;)) {
            button.textContent = &quot;Editar&quot;;
        } else {
            button.textContent = &quot;Cancelar&quot;;
        }
    }

    function chooseListToAdd(text) {
        document.body.classList.toggle(&quot; , &quot;'&quot; , &quot;no-overflow&quot; , &quot;'&quot; , &quot;);
        chooseimage = document.getElementById(&quot;add-list-&quot; + text);
        chooseimage.classList.toggle(&quot; , &quot;'&quot; , &quot;display-none&quot; , &quot;'&quot; , &quot;);
    }

    function enterCode() {
        document.body.classList.toggle(&quot; , &quot;'&quot; , &quot;no-overflow&quot; , &quot;'&quot; , &quot;);
        chooseimage = document.getElementById(&quot;enter-agrupamento&quot;);
        chooseimage.classList.toggle(&quot; , &quot;'&quot; , &quot;display-none&quot; , &quot;'&quot; , &quot;);
    }

    function chooseMemberToAdd(text) {
        document.body.classList.toggle(&quot; , &quot;'&quot; , &quot;no-overflow&quot; , &quot;'&quot; , &quot;);
        chooseimage = document.getElementById(&quot;add-member-&quot; + text);
        chooseimage.classList.toggle(&quot; , &quot;'&quot; , &quot;display-none&quot; , &quot;'&quot; , &quot;);
    }

    function searchPerson(text, agrupamentoId) { 
        var agrupamento = document.getElementById(&quot; , &quot;'&quot; , &quot;add-member-to-&quot; , &quot;'&quot; , &quot; + agrupamentoId);

        function searchInElements(element) {
            if (element.textContent.toLowerCase() === text.toLowerCase() &amp;&amp; text !== &quot; , &quot;'&quot; , &quot;&quot; , &quot;'&quot; , &quot;) {
                return true;
            }

            for (var i = 0; i &lt; element.children.length; i++) {
                var result = searchInElements(element.children[i]);
                if (result) {
                    return result;
                }
            }
            return false;
        }

        var card = null;
        for (var i = 0; i &lt; agrupamento.children.length; i++){
            if (!agrupamento.children[i].classList.contains(&quot; , &quot;'&quot; , &quot;display-none&quot; , &quot;'&quot; , &quot;)) { 
                agrupamento.children[i].classList.add(&quot; , &quot;'&quot; , &quot;display-none&quot; , &quot;'&quot; , &quot;);
            }
            var found = searchInElements(agrupamento.children[i]);
            if (found) { 
                card = agrupamento.children[i];
                break;
            }
        }

        if (card) { 
            card.classList.remove(&quot; , &quot;'&quot; , &quot;display-none&quot; , &quot;'&quot; , &quot;);
        }
    }

    function selectEdit(permissionBtn){

        var section = permissionBtn.parentNode.parentNode.parentNode

        var checkboxes = Array.from(section.querySelectorAll(&quot;.checkbox&quot;));
        var permissionUpdateBtn = section.querySelector(&quot;#permissionUpdateBtn&quot;);


        for(checkbox of checkboxes){
            if (checkbox.checked) {
                checkbox.parentNode.parentNode.querySelector(&quot;.editPermisions&quot;).style.display = &quot;block&quot;;
            }
        }

        permissionBtn.style.display = &quot;none&quot;;
        permissionUpdateBtn.style.display = &quot;block&quot;;
    }

    function checkTheBox(checkbox,idAgrupamento){
        
        var section = checkbox.parentNode.parentNode.parentNode.parentNode.parentNode
        var variavel = &quot;#formRemove_&quot; + idAgrupamento;
        console.log(variavel);
        var checkboxes = Array.from(section.querySelectorAll(&quot;.checkbox&quot;));

        var permissionUpdateBtn = section.querySelector(&quot;#permissionBtn&quot;);
        var removeBtn = section.querySelector(&quot;#formRemove_&quot;+idAgrupamento);
        var addBtn = section.querySelector(&quot;#addBtn&quot;);

        permissionUpdateBtn.style.display = &quot;none&quot;;
        removeBtn.style.display = &quot;none&quot;;
        addBtn.style.display = &quot;block&quot;;

        for (checkbox of checkboxes) {
            if (checkbox.checked) {

                var permissionUpdateBtn = section.querySelector(&quot;#permissionBtn&quot;);
                var removeBtn = section.querySelector(&quot;#formRemove_&quot;+idAgrupamento);

                permissionUpdateBtn.style.display = &quot;block&quot;;
                removeBtn.style.display = &quot;block&quot;;
                addBtn.style.display = &quot;none&quot;;
            }
        }
    }

    document.addEventListener(&quot; , &quot;'&quot; , &quot;DOMContentLoaded&quot; , &quot;'&quot; , &quot;, function () {
        const specificDiv = document.getElementById(&quot; , &quot;'&quot; , &quot;mensagem&quot; , &quot;'&quot; , &quot;);

        function displayMessage(statusMessage) {
            const statusElement = document.createElement(&quot; , &quot;'&quot; , &quot;div&quot; , &quot;'&quot; , &quot;);
            statusElement.classList.add(&quot; , &quot;'&quot; , &quot;alert&quot; , &quot;'&quot; , &quot;, &quot; , &quot;'&quot; , &quot;alert-success&quot; , &quot;'&quot; , &quot;, &quot; , &quot;'&quot; , &quot;status-message&quot; , &quot;'&quot; , &quot;);
            statusElement.textContent = statusMessage;

            specificDiv.innerHTML = &quot; , &quot;'&quot; , &quot;&quot; , &quot;'&quot; , &quot;; 
            specificDiv.appendChild(statusElement);

            const displayTime = 3000;
            setTimeout(() => {
                statusElement.style.display = &quot; , &quot;'&quot; , &quot;none&quot; , &quot;'&quot; , &quot;;
            }, displayTime);
        }

        const statusMessage = &quot; , &quot;'&quot; , &quot;Membro[s] removido[s] com sucesso!&quot; , &quot;'&quot; , &quot;;

        if (statusMessage) {
            displayMessage(statusMessage);
        }
    });
    





Agrupamentos
Membro[s] removido[s] com sucesso!

    
        Criar Agrupamento
    
    
        Entrar Agrupamento
    



    
        
            Entrar num Agrupamento
            
        
        
            
                Código
                
            
            
                Entrar
            
        
    



        
            
                Agrupamento:P 
                
            

            
                
                    
                       
                            
                            Copiar Código
                        
                        
                            Eliminar Agrupamento
                        
                    
                    
                        
                            
                                Eliminar Agrupamento
                                
                            

                            
                                Tem certeza que deseja eliminar este agrupamento?
                                
                                    Agrupamento:P   
                                

                                
                                    

                                        

                                             Eliminar Agrupamento

                                        
                                    

                                
                            
                        

                    

                
                    
                        
                        
                            
                            Nome
                            
                        
                        
                            
                                Renomear
                           
                        
                    
                

                
                
                    
                        Listas
                            

                                Adicionar Lista
                                

                                    

                                
                            
                       
                    
                    
                    
                        
                        
                    

                    
                        
                            
                                Adicionar uma Lista
                                
                             
                            
                           
                            
                                
                                Lista de Produtos
                                
                                            
                                                
                                                Lista de Sexta-feira
                                            
                                
                                
                                    Salvar
                                
                            
                        
                    
                

                
                
                    
                


                
                    
                        Membros

                            


                                
                                
                                

                                
                                    
                                

                            
                      
                    
                    
                    
                        
                                            Administradores
                                        Cliente:P
                                            Leitores
                                            
                                            
                                                    
                                                Luis
                                            
                                            
                                                Selecionar Permissão:
                                                
                                                    Leitor
                                                    Editor
                                                    Leitor
                                                
                                            
                                        
                        
                    

                    
                        
                            
                                Adicionar um Membro
                                
                            
                            
                                Pesquise um membro
                                
                                    
                                    Copiar Código
                                
                                
                                    
                                        
                                            
                                            
                                        
                                    
                                    
                                        
                                                    
                                                        
                                                            
                                                            Rodrigo
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            BlitzGB
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            i3ggg
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Matilde
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            luis.goias.1999@gmail.com
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Afonso
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            &lt;h1>Lucas&lt;/h1>
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            ccroyale34@gmail.com
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Cid
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Alexandre
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Duarte
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            yonaxiy
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            xegoron
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            jacinto
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            André Lisboa
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            marta
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Andreia
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Carlos
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Felipe T. Peres
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            testeuser1
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            joao
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Janaina
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            jana.bruno@gmail.com
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            vanessa venites
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Inês Pereira
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Eunice do Carmo
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Nihen
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Rui
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            22
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Util
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Util
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Util
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Automacao
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Automacao
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Automacao
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                        
                                    
                                
                                
                                    Ok
                                
                            
                        
                    
                
            
            
        
        
            
                Agrupamento 1
                
            

            
                

                
                    
                        
                        
                            
                            Nome
                            
                        
                        
                            
                           
                        
                    
                

                
                
                    
                        Listas
                            

                                Adicionar Lista
                                

                                    

                                
                            
                       
                    
                    
                    
                        
                        
                    

                    
                        
                            
                                Adicionar uma Lista
                                
                             
                            
                           
                            
                                
                                Lista de Produtos
                                
                                            
                                                
                                                Lista de Sexta-feira
                                            
                                
                                
                                    Salvar
                                
                            
                        
                    
                

                
                
                    
                


                
                    
                        Membros

                      
                    
                    
                    
                        
                                            Administradores
                                        Luis
                                            Editores
                                        
                                            
                                                
                                                Cliente:P
                                            
                                            
                                                Selecionar Permissão:
                                                
                                                    Editor
                                                    Editor
                                                    Leitor
                                                
                                            
                                        
                        
                    

                    
                        
                            
                                Adicionar um Membro
                                
                            
                            
                                Pesquise um membro
                                
                                    
                                    Copiar Código
                                
                                
                                    
                                        
                                            
                                            
                                        
                                    
                                    
                                        
                                                    
                                                        
                                                            
                                                            Rodrigo
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            BlitzGB
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            i3ggg
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Matilde
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            luis.goias.1999@gmail.com
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Afonso
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            &lt;h1>Lucas&lt;/h1>
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            ccroyale34@gmail.com
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Cid
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Alexandre
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Duarte
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            yonaxiy
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            xegoron
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            jacinto
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            André Lisboa
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            marta
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Andreia
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Carlos
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Felipe T. Peres
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            testeuser1
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            joao
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Janaina
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            jana.bruno@gmail.com
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            vanessa venites
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Inês Pereira
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Eunice do Carmo
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Nihen
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Rui
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            22
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Util
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Util
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Util
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Automacao
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Automacao
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                                    
                                                        
                                                            
                                                            Automacao
                                                        
                                                        
                                                            
                                                            
                                                            Adicionar Membro
                                                        
                                                    
                                        
                                    
                                
                                
                                    Ok
                                
                            
                        
                    
                
            
            
        





    document.addEventListener(&quot;DOMContentLoaded&quot;,
        tooltipmsg(&quot;Nesta página, poderá gerir, criar e participar em grupos onde poderá partilhar as suas listas com os membros do grupo.&quot;,&quot;&quot;,&quot;&quot;)
    );

    function collapse(coll) {
        coll.querySelector(&quot;.colapsable-button&quot;).classList.toggle(&quot;active&quot;);
        var content = coll.nextElementSibling;
        if (content.style.maxHeight) {
            content.style.maxHeight = null;
        } else {
            console.log(content.scrollHeight)
            content.style.maxHeight = content.scrollHeight + &quot;px&quot;;
        }
    }

    function removerMembro(){
        document.getElementById(&quot;hideremove&quot;).style.display = &quot;none&quot;;
        document.getElementById(&quot;showremove&quot;).style.display = &quot;inherit&quot;;
        document.getElementById(&quot;showboxes&quot;).style.display = &quot;inherit&quot;;


    }

    function changeNameEditableField(button) {
        var form = button.parentNode.parentNode;
        var elemtToHide = button.parentNode.children[0];

        var inputs = form.querySelectorAll(&quot; , &quot;'&quot; , &quot;input&quot; , &quot;'&quot; , &quot;);
        inputs.forEach(function (input) {
            if (input.id === &quot; , &quot;'&quot; , &quot;name-input&quot; , &quot;'&quot; , &quot;) {
                input.disabled = !input.disabled;
            }
        });

        var elements = document.querySelectorAll(&quot; , &quot;'&quot; , &quot;.authentication-input-text-div&quot; , &quot;'&quot; , &quot;);
        elements.forEach(function (element) {

            if (element.querySelector(&quot; , &quot;'&quot; , &quot;span.text-danger&quot; , &quot;'&quot; , &quot;)) {
                if (element.querySelector(&quot; , &quot;'&quot; , &quot;span.text-danger&quot; , &quot;'&quot; , &quot;).textContent === &quot; , &quot;'&quot; , &quot;&quot; , &quot;'&quot; , &quot;) {
                    element.style.height = element.children[0].getBoundingClientRect().height + &quot; , &quot;'&quot; , &quot;px&quot; , &quot;'&quot; , &quot;;
                }
            }
        });

        elemtToHide.classList.toggle(&quot; , &quot;'&quot; , &quot;off-screen&quot; , &quot;'&quot; , &quot;);

        if (elemtToHide.classList.contains(&quot; , &quot;'&quot; , &quot;off-screen&quot; , &quot;'&quot; , &quot;)) {
            button.textContent = &quot;Editar&quot;;
        } else {
            button.textContent = &quot;Cancelar&quot;;
        }
    }

    function chooseListToAdd(text){ 
        document.body.classList.toggle(&quot; , &quot;'&quot; , &quot;no-overflow&quot; , &quot;'&quot; , &quot;);
        chooseimage = document.getElementById(&quot;add-list-&quot;+text);
        console.log(text);
        console.log(&quot;add-list-&quot; + text);
        console.log(chooseimage);

        chooseimage.classList.toggle(&quot; , &quot;'&quot; , &quot;display-none&quot; , &quot;'&quot; , &quot;);
    }

    function deletePopup(text) {
        document.body.classList.toggle(&quot; , &quot;'&quot; , &quot;no-overflow&quot; , &quot;'&quot; , &quot;);
        chooseimage = document.getElementById(&quot;delete-popup-&quot; + text);
        chooseimage.classList.toggle(&quot; , &quot;'&quot; , &quot;display-none&quot; , &quot;'&quot; , &quot;);
    }




                        &quot;))]</value>
      <webElementGuid>10679fac-5b77-494e-a74f-0f6d1ca6c948</webElementGuid>
   </webElementXpaths>
</WebElementEntity>
