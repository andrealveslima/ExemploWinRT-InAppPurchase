using GalaSoft.MvvmLight;

namespace ExemploWinRT_InAppPurchase.ViewModel
{
    /// <summary>
    /// ViewModel contendo as propriedades e comandos a serem bindados na Main View.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region Atributos e Propriedades
        /// <summary>
        /// Propriedade que exp�e a inst�ncia Singleton de LicenseHelper.
        /// </summary>
        public LicenseHelper LicenseHelper
        {
            get
            {
                return ExemploWinRT_InAppPurchase.LicenseHelper.Current;
            }
        }
        /// <summary>
        /// Atalho para a propriedade AnuncioRemovido da classe LicenseHelper. Adicionada na ViewModel para suportar change notification quando
        /// o usu�rio compra a licen�a de "ANUNCIO_REMOVIDO".
        /// </summary>
        public bool AnuncioRemovido
        {
            get
            {
                return LicenseHelper.AnuncioRemovido;
            }
        }
        #endregion

        #region Construtores
        /// <summary>
        /// Constroi uma inst�ncia de MainViewModel, inicializando os comandos necess�rios.
        /// </summary>
        public MainViewModel()
        {
            RemoverAnuncio = new GalaSoft.MvvmLight.Command.RelayCommand(RemoverAnuncioAction, CanExecuteRemoverAnuncio);
        }
        #endregion

        #region Comandos
        #region Remover An�ncio
        /// <summary>
        /// Comando que pode ser utilizado para realizar a compra da licen�a de remo��o do an�ncio.
        /// </summary>
        public GalaSoft.MvvmLight.Command.RelayCommand RemoverAnuncio { get; private set; }
        /// <summary>
        /// A��o que � executada quando o comando RemoverAnuncio � disparado.
        /// </summary>
        private async void RemoverAnuncioAction()
        {
            await LicenseHelper.RemoverAnuncioAsync().ContinueWith(
                (task) => GalaSoft.MvvmLight.Threading.DispatcherHelper.RunAsync(
                    () =>
                    {
                        RaisePropertyChanged("AnuncioRemovido");
                        RemoverAnuncio.RaiseCanExecuteChanged();
                    }));
        }
        /// <summary>
        /// Indica se o comando RemoverAnuncio pode ser executado ou n�o.
        /// Ele pode ser executado quando a licen�a da remo��o do an�ncio ainda n�o tiver sido comprada.
        /// </summary>
        /// <returns>True se o comando RemoverAnuncio pode ser executado. Sen�o retorna false.</returns>
        private bool CanExecuteRemoverAnuncio()
        {
            return !LicenseHelper.AnuncioRemovido;
        }
        #endregion
        #endregion
    }
}