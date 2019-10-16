



var AuthFunctions =
{
    $impl: {
        openAuthUI: function()
                  {
                    console.log('test');
                    const provider = new firebase.auth.GoogleAuthProvider();
                    firebase.auth().signInWithPopup(provider).then(function(result) {
            
                      // The signed-in user info.
                      var user = result.user;
                      user.getIdToken().then((token) => {
                  
                            const successObj = {
                                accessToken: result.credential.accessToken,
                                idToken: token
                            }
                                       
                            unityInstance.SendMessage("AuthHandler", "GoogleLoginSuccess", JSON.stringify(successObj));
                            
                      });
                     
                    }).catch(function(error) {
                      console.log(error);
                      // Handle Errors here.
                      var errorCode = error.code;
                      var errorMessage = error.message;
                      // The email of the user's account used.
                      var email = error.email;
                      // The firebase.auth.AuthCredential type that was used.
                      var credential = error.credential;
          
                      
                      unityInstance.SendMessage("AuthHandler", "GoogleLoginError", JSON.stringify(error));
                    });
                  },
    },
 
    OpenAuthUI: function ()
    {
        impl.openAuthUI();
    },
};
 
autoAddDeps(AuthFunctions, '$impl');
mergeInto(LibraryManager.library, AuthFunctions);
