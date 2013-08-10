using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrentAppInfo = Windows.ApplicationModel.Store.CurrentAppSimulator; // Declaração para facilitar a troca entre CurrentApp e CurrentAppSimulator.

namespace ExemploWinRT_InAppPurchase
{
    /// <summary>
    /// Classe auxiliar para gerenciar as licenças do produto.
    /// </summary>
    public class LicenseHelper
    {
        #region Singleton
        /// <summary>
        /// Atributo privado que contém a instância do Singleton.
        /// </summary>
        private LicenseHelper _current;
        /// <summary>
        /// Propriedade que expõe a instância do Singleton.
        /// </summary>
        public LicenseHelper Current
        {
            get
            {
                if (_current == null)
                    _current = new LicenseHelper();
                return _current;
            }
        }
        /// <summary>
        /// Construtor parameterless privado para assegurar o pattern Singleton.
        /// </summary>
        private LicenseHelper() { }
        #endregion

        #region Atributos e Propriedades
        /// <summary>
        /// Retorna a licença (LicenseInformation) de CurrentApp (ou CurrentAppSimulator quando o simulador está sendo utilizado).
        /// </summary>
        public Windows.ApplicationModel.Store.LicenseInformation Licenca
        {
            get
            {
                return CurrentAppInfo.LicenseInformation;
            }
        }
        /// <summary>
        /// Indica se o usuário atual adquiriu a licença "ANUNCIO_REMOVIDO" ou não.
        /// </summary>
        public bool AnuncioRemovido
        {
            get
            {
                return Licenca.ProductLicenses["ANUNCIO_REMOVIDO"].IsActive;
            }
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Solicita a compra da licença "ANUNCIO_REMOVIDO" caso o usuário ainda não a tenha adquirido.
        /// </summary>
        /// <returns>A async Task do processo de compra da licença.</returns>
        public async Task RemoverAnuncioAsync()
        {
            try
            {
                if (!AnuncioRemovido)
                    await CurrentAppInfo.RequestProductPurchaseAsync("ANUNCIO_REMOVIDO", false);
            }
            catch { }
        }
        #endregion
    }
}
